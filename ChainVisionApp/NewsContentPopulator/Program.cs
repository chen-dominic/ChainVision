using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ChainVision.Data;
using ChainVision.Data.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddDbContext<ChainVisionContext>(options =>
            options.UseSqlServer("Server=1235dc-sqldev;Database=ChainVision;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"));

        var serviceProvider = services.BuildServiceProvider();
        var dbContext = serviceProvider.GetService<ChainVisionContext>();
        Timer timer = new Timer(async _ =>
        {
            await RunJobAsync(dbContext);
        }, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

        // Keep the application running
        await Task.Delay(Timeout.Infinite);
    }

    static async Task RunJobAsync(ChainVisionContext dbContext)
    {
        try
        {
            var articlesData = await GenerateNewsData(dbContext);

            foreach (var article in articlesData)
            {
                ProccessedData processedArticle;
                // 2. Process the article through your Python AI model (Flask API)
                try
                {
                    processedArticle = await ProcessNewsData(article);
                }
                catch (Exception ex) {
                    string[] ingredient = { "N/A" };
                    processedArticle = new ProccessedData
                    {
                        risk_score = 6,
                        ingredients = ingredient,
                        risk_level = "Unknown"
                    };
                }

                    // 3. populate database with news data and disruption tables
                    var newsDb = new News
                {
                    ArticleId = article.article_id,
                    Title = article.title,
                    DisplayText = article.description,
                    Description = article.content,
                    SeverityRatingId = (byte?)processedArticle.risk_score,
                    PublishedDateUtc = article.pubDate,
                    Country = string.Join(", ", article.country),
                    ArticleUrl = article.link
                };
                dbContext.News.Add(newsDb);
                await dbContext.SaveChangesAsync();
                var newsId = dbContext.News.Where(_ => _.ArticleId ==  article.article_id).Select(_ => _.Id).FirstOrDefault();
                var severityId = newsDb.SeverityRatingId;

                var materials = processedArticle.ingredients.Distinct();
                foreach(var material in materials)
                {
                    var matId = dbContext.Materials.Where(_ => _.MaterialName == material).Select(_ => _.Id).FirstOrDefault();
                    if(matId == null || matId == 0)
                    {
                        var newMat = new Material
                        {
                            MaterialName = material
                        };
                        dbContext.Materials.Add(newMat);
                        await dbContext.SaveChangesAsync();
                        matId = dbContext.Materials.Where(_ => _.MaterialName == material).Select(_ => _.Id).FirstOrDefault();
                    }
                    var matDisrupt = new MaterialDisruption
                    {
                        MaterialId = matId,
                        NewsId = newsId,
                        DisruptionId = processedArticle.risk_score
                    };
                    dbContext.MaterialDisruptions.Add(matDisrupt);
                }

                var currentTime = DateTime.UtcNow;
                dbContext.UpdatedTimes.Add(new UpdatedTime { LastUpdatedTimeUtc = currentTime });

                await dbContext.SaveChangesAsync();
                Console.WriteLine($"Inserted news: {article.title}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error running job: {ex.Message}");
        }
        Console.WriteLine("\n\n");
    }

    static async Task<List<NewsArticle>> GenerateNewsData(ChainVisionContext dbContext)
    {
        Console.WriteLine("Starting Generation...");

        var openAiApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

        if (string.IsNullOrEmpty(openAiApiKey))
        {
            throw new Exception("OpenAI API key not found in environment variables.");
        }

        Console.WriteLine($"OPEN AI KEY: {openAiApiKey}");
        Console.WriteLine("---------------------------------");
            
        using (var client = new HttpClient())
        {
            string existingArticleIds = $"( {string.Join(",", dbContext.News.Select(_ => _.ArticleId).ToList())} )";
            string ingredientsList = $"( {string.Join(", ", dbContext.Materials.Select(_ => _.MaterialName).ToList())} )";
            var yesterday = DateTime.Now.Date.AddDays(-1);
            var today = DateTime.Now.Date;
            string jsonFormatText = "{" +
                $"Article_Id (string, different from any of these: {existingArticleIds})," +
                $"Title (string, title of article)," +
                $"Link (string, URL of article)," +
                $"Keywords (string[], array of keywords related to supply chain)," +
                $"Description (string, general summary of article, 1-2 sentences)," +
                $"Content (string, full article body)," +
                $"PubDate (DateTime, article published date, must be between {yesterday} and {today})," +
                $"Country (string[], array of countries related to article)" +
                "}";

            string generatorQuery =
                $"Generate 3 news articles in the form of a list of this JSON: {jsonFormatText}. Make it related to supply chain, " +
                $"with varying severity, and (optional) to be related to this ingredient list {ingredientsList}. ";

            Console.WriteLine($"Generator Query: {generatorQuery}");

            var requestBody = new
            {
                model = "gpt-4",
                messages = new[]
                {
                new { role = "system", content = "You are a powerful news API generating news articles." },
                new { role = "user", content = generatorQuery }
            },
                max_tokens = 3000, // Adjust token limit as needed
                temperature = 0.7 // Adjust creativity
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {openAiApiKey}");

            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);

            if (!response.IsSuccessStatusCode)
            {
                string errorResponse = await response.Content.ReadAsStringAsync();
                throw new Exception($"OpenAI API call failed: {errorResponse}");
            }

            // Parse the OpenAI response
            var openAiResponse = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            Console.WriteLine($"Raw OpenAI Response: {openAiResponse}");

            string generatedText = openAiResponse?.choices[0]?.message?.content;

            if (string.IsNullOrEmpty(generatedText))
            {
                throw new Exception("No response generated by OpenAI.");
            }

            string cleanedText = Regex.Replace(generatedText, @"^\d+\.\s*", "", RegexOptions.Multiline); // Remove numeric prefixes

            if (!cleanedText.Trim().StartsWith("["))
            {
                cleanedText = $"[{cleanedText}]";
            }

            string fixedJson = Regex.Replace(
                cleanedText,
                @"}\s*{",  // Match `}{` or `}\n{` patterns
                "},{",     // Replace with `},{` to fix the JSON structure
                RegexOptions.Multiline
            );

            // Clean up the string by removing markdown code block markers
            string cleanedJson = Regex.Replace(fixedJson, @"```json", "").Replace("```", "");

            // Now you have a proper JSON string
            Console.WriteLine($"Cleaned Generated Text:\n{cleanedJson}");

            // Deserialize it into your list of articles
            List<NewsArticle> articles;
            try
            {
                articles = JsonConvert.DeserializeObject<List<NewsArticle>>(cleanedJson);
            }
            catch (JsonException ex)
            {
                throw new Exception($"Failed to deserialize articles: {ex.Message}. Raw response: {cleanedJson}");
            }

            Console.WriteLine($"Cleaned Generated Text: {articles.ToString()}");
            return articles;


        }
    }

    static async Task<ProccessedData> ProcessNewsData(NewsArticle article)
    {
        using (var client = new HttpClient())
        {
            var content = new StringContent(JsonConvert.SerializeObject(article), Encoding.UTF8, "application/json");

            // Call your Python Flask AI model API
            var response = await client.PostAsync("http://10.35.61.54:5001/api/v1/risk-assessment", content);
            response.EnsureSuccessStatusCode();

            // Deserialize the processed article data
            var processedData = JsonConvert.DeserializeObject<ProccessedData>(await response.Content.ReadAsStringAsync());

            int risk = processedData.risk_score;
            if(risk < 3)
            {
                processedData.risk_score = 1;
            }
            else if(risk < 6)
            {
                processedData.risk_score = 2;
            }
            else if(risk < 9)
            {
                processedData.risk_score = 3;
            }
            else if (risk < 12)
            {
                processedData.risk_score = 4;
            }
            else
            {
                processedData.risk_score = 5;

            }
            return processedData;
        }
    }

    public class NewsArticle
    {
        public string article_id { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string[] keywords { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public DateTime pubDate { get; set; }
        public string[] country { get; set; }
        public string sentitment { get; set; }
    }

    public class ProccessedData
    {
        public string[] ingredients { get; set; }
        public string cost_increase { get; set; }
        public string risk_level { get; set; }
        public int risk_score { get; set; }
    }
}
