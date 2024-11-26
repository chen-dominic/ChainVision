public class FGFSupplyChainIngredients
{
    // Comprehensive ingredient mapping for FGF products
    public Dictionary<string, IngredientDetails> CriticalIngredients = new Dictionary<string, IngredientDetails>
    {
        {
            "Wheat Flour", new IngredientDetails
            {
                Name = "Wheat Flour",
                ProductCategories = new List<string> 
                {
                    "Naan", "Pizza Crusts", "Flatbreads", 
                    "Buns", "Rolls", "Loaves", "Muffins", "Croissants"
                },
                CriticalityScore = 0.9,
                PrimarySourceCountries = new Dictionary<string, float>
                {
                    { "Canada", 0.7 },
                    { "USA", 0.6 },
                    { "Ukraine", 0.5 },
                    { "Argentina", 0.4 }
                }
            }
        },
        {
            "Yeast", new IngredientDetails
            {
                Name = "Yeast",
                ProductCategories = new List<string> 
                {
                    "Naan", "Bread", "Pizza Crusts", "Muffins", "Croissants"
                },
                CriticalityScore = 0.7,
                PrimarySourceCountries = new Dictionary<string, float>
                {
                    { "USA", 0.6 },
                    { "Netherlands", 0.5 },
                    { "France", 0.4 }
                }
            }
        },
        {
            "Garlic", new IngredientDetails
            {
                Name = "Garlic",
                ProductCategories = new List<string> 
                {
                    "Roasted Garlic Naan", "Garlic Naan Crisps"
                },
                CriticalityScore = 0.5,
                PrimarySourceCountries = new Dictionary<string, float>
                {
                    { "China", 0.7 },
                    { "India", 0.6 },
                    { "Egypt", 0.5 }
                }
            }
        },
        {
            "Milk", new IngredientDetails
            {
                Name = "Milk",
                ProductCategories = new List<string> 
                {
                    "Cheese Croissants", "Muffins"
                },
                CriticalityScore = 0.6,
                PrimarySourceCountries = new Dictionary<string, float>
                {
                    { "Canada", 0.7 },
                    { "USA", 0.6 },
                    { "New Zealand", 0.5 }
                }
            }
        },
        {
            "Cocoa", new IngredientDetails
            {
                Name = "Cocoa",
                ProductCategories = new List<string> 
                {
                    "Chocolate Muffins"
                },
                CriticalityScore = 0.8,
                PrimarySourceCountries = new Dictionary<string, float>
                {
                    { "Ghana", 0.7 },
                    { "Ivory Coast", 0.6 },
                    { "Brazil", 0.5 }
                }
            }
        }
    };

    public class IngredientDetails
    {
        public string Name { get; set; }
        public List<string> ProductCategories { get; set; }
        public double CriticalityScore { get; set; }
        public Dictionary<string, float> PrimarySourceCountries { get; set; }
    }

    // Risk Assessment Method
    public SupplyChainRiskAssessment AssessSupplyChainRisk(NewsArticle article)
    {
        var riskFactors = new List<RiskFactor>();

        // Check each critical ingredient
        foreach (var ingredient in CriticalIngredients)
        {
            // Check if article mentions ingredient or its source countries
            bool isRelevant = 
                article.Content.Contains(ingredient.Key, StringComparison.OrdinalIgnoreCase) ||
                ingredient.Value.PrimarySourceCountries.Keys.Any(country => 
                    article.Content.Contains(country, StringComparison.OrdinalIgnoreCase));

            if (isRelevant)
            {
                var riskFactor = new RiskFactor
                {
                    Ingredient = ingredient.Key,
                    AffectedProducts = ingredient.Value.ProductCategories,
                    CriticalityScore = ingredient.Value.CriticalityScore
                };

                // Determine risk level based on article content
                riskFactor.RiskLevel = DetermineRiskLevel(article, ingredient.Value);

                riskFactors.Add(riskFactor);
            }
        }

        return new SupplyChainRiskAssessment
        {
            Article = article,
            RiskFactors = riskFactors,
            OverallRiskScore = CalculateOverallRiskScore(riskFactors)
        };
    }

    private float DetermineRiskLevel(NewsArticle article, IngredientDetails ingredient)
    {
        // Risk keywords with their impact scores
        var riskKeywords = new Dictionary<string, float>
        {
            { "trade ban", 0.9 },
            { "export restriction", 0.8 },
            { "crop failure", 0.7 },
            { "production halt", 0.8 },
            { "embargo", 0.9 },
            { "climate impact", 0.6 }
        };

        // Check for risk keywords
        float keywordRisk = riskKeywords
            .Where(kw => article.Content.Contains(kw.Key, StringComparison.OrdinalIgnoreCase))
            .Sum(kw => kw.Value);

        // Combine with ingredient criticality
        return (float)Math.Min(
            (keywordRisk + ingredient.CriticalityScore) / 2, 
            1.0
        );
    }

    private float CalculateOverallRiskScore(List<RiskFactor> riskFactors)
    {
        return riskFactors.Any() 
            ? (float)riskFactors.Average(rf => rf.RiskLevel)
            : 0f;
    }

    // Supporting Classes
    public class SupplyChainRiskAssessment
    {
        public NewsArticle Article { get; set; }
        public List<RiskFactor> RiskFactors { get; set; }
        public float OverallRiskScore { get; set; }
    }

    public class RiskFactor
    {
        public string Ingredient { get; set; }
        public List<string> AffectedProducts { get; set; }
        public double CriticalityScore { get; set; }
        public float RiskLevel { get; set; }
    }
}