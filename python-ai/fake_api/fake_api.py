from flask import Flask, jsonify

# Create Flask app
app = Flask(__name__)

# Sample data to mimic news data
sample_news = [
    # Existing articles
    {
        "article_id": "30b8169c8ce08d7e5b491fdc4a039d67",
        "title": "Trade Ban on Wheat Flour from Ukraine",
        "link": "https://example.com/article1",
        "keywords": ["wheat", "flour", "trade", "ban"],
        "description": "A severe trade ban on wheat flour exports from Ukraine has been announced.",
        "content": "A severe trade ban on wheat flour exports from Ukraine has been announced, causing major disruptions to global supply chains.",
        "pubDate": "2024-11-27 02:56:44",
        "country": ["malaysia"],
        "sentiment": "negative",
    },
    # New articles (Very Low to Critical)
    {
        "article_id": "1",
        "title": "Local Bakery Introduces New Gluten-Free Bread Line",
        "link": "https://example.com/article6",
        "keywords": ["bread", "gluten-free", "local"],
        "description": "A local bakery has launched a new line of gluten-free bread to cater to health-conscious consumers.",
        "content": "A local bakery introduces a gluten-free bread line. No supply chain impact is reported.",
        "pubDate": "2024-11-20 09:00:00",
        "country": ["united states"],
        "sentiment": "positive",
    },
    {
        "article_id": "2",
        "title": "Minor Delay in Sugar Delivery Due to Traffic Congestion",
        "link": "https://example.com/article7",
        "keywords": ["sugar", "delivery", "traffic"],
        "description": "A temporary traffic issue caused a slight delay in sugar delivery to local bakeries.",
        "content": "Traffic congestion caused a delay in sugar delivery. Operations continue with minor adjustments.",
        "pubDate": "2024-11-21 10:30:00",
        "country": ["canada"],
        "sentiment": "neutral",
    },
    {
        "article_id": "3",
        "title": "Bakery Workers Vote to Unionize, Negotiations Begin",
        "link": "https://example.com/article8",
        "keywords": ["bakery", "workers", "unionize", "negotiations"],
        "description": "Employees at a regional bakery have voted to unionize, initiating negotiations.",
        "content": "A vote to unionize has been announced. It may affect bakery worker schedules, with possible disruptions to operations.",
        "pubDate": "2024-11-22 08:15:00",
        "country": ["united states"],
        "sentiment": "neutral",
    },
    {
        "article_id": "4",
        "title": "Rising Butter Prices Affecting Bakery Margins",
        "link": "https://example.com/article9",
        "keywords": ["butter", "prices", "bakery", "economy"],
        "description": "An increase in butter prices is squeezing profit margins for bakeries.",
        "content": "Rising butter prices have created challenges for bakeries. Adjustments to pricing models may be required.",
        "pubDate": "2024-11-23 11:00:00",
        "country": ["global"],
        "sentiment": "negative",
    },
    {
        "article_id": "5",
        "title": "Flour Mill Maintenance Causes Temporary Supply Shortage",
        "link": "https://example.com/article10",
        "keywords": ["flour", "shortage", "maintenance"],
        "description": "Scheduled maintenance at a major flour mill has led to a short-term shortage.",
        "content": "Maintenance at a key flour mill has disrupted supplies temporarily. Bakeries may experience short-term delays.",
        "pubDate": "2024-11-24 14:30:00",
        "country": ["india"],
        "sentiment": "negative",
    },
    {
        "article_id": "6",
        "title": "Labor Strike at Yeast Production Plant Disrupts Supply",
        "link": "https://example.com/article11",
        "keywords": ["yeast", "strike", "labor", "disruptions"],
        "description": "A labor strike at a yeast production plant has disrupted supply chains.",
        "content": "Labor strikes at a yeast plant have created bottlenecks in supply chains, impacting bakery operations.",
        "pubDate": "2024-11-25 09:00:00",
        "country": ["germany"],
        "sentiment": "negative",
    },
    {
        "article_id": "7",
        "title": "Severe Drought Reduces Wheat Harvest by 30%",
        "link": "https://example.com/article12",
        "keywords": ["wheat", "drought", "harvest"],
        "description": "Ongoing drought conditions have significantly reduced wheat yields.",
        "content": "Severe drought conditions have led to a 30% reduction in wheat harvest, increasing costs for bakeries.",
        "pubDate": "2024-11-26 13:15:00",
        "country": ["australia"],
        "sentiment": "negative",
    },
    {
        "article_id": "8",
        "title": "Government Imposes Tariff on Imported Sugar",
        "link": "https://example.com/article13",
        "keywords": ["sugar", "tariffs", "economy"],
        "description": "New government tariffs on imported sugar are expected to raise costs for bakeries.",
        "content": "Government tariffs on sugar imports have been introduced, significantly impacting bakery supply costs.",
        "pubDate": "2024-11-27 15:45:00",
        "country": ["united states"],
        "sentiment": "negative",
    },
    {
        "article_id": "9",
        "title": "Global Vanilla Shortage Drives Up Prices",
        "link": "https://example.com/article14",
        "keywords": ["vanilla", "shortage", "prices"],
        "description": "A worldwide vanilla shortage has caused prices to skyrocket.",
        "content": "Vanilla shortages have disrupted bakery supply chains globally, with prices reaching record highs.",
        "pubDate": "2024-11-28 17:30:00",
        "country": ["madagascar"],
        "sentiment": "negative",
    },
    {
        "article_id": "10",
        "title": "Flooding in Key Agricultural Regions Disrupts Grain Supply",
        "link": "https://example.com/article15",
        "keywords": ["flooding", "agriculture", "grain", "supply"],
        "description": "Severe flooding in major agricultural areas has disrupted grain supplies.",
        "content": "Flooding in key regions has disrupted grain supplies, leading to shortages and increased costs for bakeries.",
        "pubDate": "2024-11-29 19:00:00",
        "country": ["pakistan"],
        "sentiment": "negative",
    },
]

# API route to fetch sample news
@app.route("/api/v1/news", methods=["GET"])
def get_news():
    return jsonify({"status": "success", "totalResults": len(sample_news), "results": sample_news})

# Start Flask app
if __name__ == "__main__":
    app.run(debug=True)
