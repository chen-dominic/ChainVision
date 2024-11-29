import logging
from src.data_preprocessing.data_loader import SupplyChainDataLoader
from src.models.risk_assessment_model import RiskAssessment
from src.data_preprocessing.text_preprocessor import TextPreprocessor
import yaml
import requests

# Configure logging
logging.basicConfig(level=logging.INFO)

def load_config(filepath="config/config.yaml"):
    """
    Load configuration from a YAML file.
    """
    try:
        with open(filepath, "r") as file:
            return yaml.safe_load(file)
    except Exception as e:
        logging.error(f"Error loading config file: {e}")
        return {}

def fetch_news(api_url):
    """
    Fetch news articles from the specified API.
    """
    try:
        response = requests.get(api_url)
        response.raise_for_status()
        return response.json()
    except Exception as e:
        logging.error(f"Error fetching news: {e}")
        return {}

def process_article(article, risk_assessment, preprocessor):
    """
    Processes a single article and logs the risk assessment results.
    """
    try:
        title = article.get("title", "No Title Available")
        content = preprocessor.preprocess(article.get("content", "No Content Available"))
        keywords = article.get("keywords", [])
        description = article.get("description", "No Description Available")
        link = article.get("link", "No Link")
        country = article.get("country", "No Country")
        pub_date = article.get("pubDate", "No Date")

        # Risk analysis
        risk_score = risk_assessment.assess_article(content)
        risk_level = risk_assessment.get_risk_level(risk_score)

        # Extract ingredients
        ingredients = risk_assessment.extract_ingredients(content)
        ingredients = ", ".join(ingredients) if ingredients else "None"

        # Log article details
        logging.info(f"Title: {title}")
        logging.info(f"Link: {link}")
        logging.info(f"Risk Level: {risk_level}")
        logging.info(f"Risk Score: {risk_score}")
        logging.info(f"Ingredients: {ingredients}")
        logging.info(f"Description: {description}")
        logging.info(f"Country: {country}")
        logging.info(f"Published Date: {pub_date}")
        logging.info("-" * 50)

    except Exception as e:
        logging.error(f"Error processing article: {e}")

def main():
    logging.info("Starting ChainVision...")

    # Load configuration
    config = load_config("config/config.yaml")

    # Initialize components
    risk_assessment = RiskAssessment(
        risk_keywords=config.get("risk_keywords", {}),
        critical_ingredients=config.get("critical_ingredients", {})
    )
    preprocessor = TextPreprocessor()

    # Fetch news data
    api_url = "http://127.0.0.1:5000/api/v1/news"
    news_data = fetch_news(api_url)

    # Process news articles
    if news_data.get("status") == "success":
        logging.info(f"Total Articles Found: {news_data.get('totalResults', 0)}")
        for article in news_data.get("results", []):
            process_article(article, risk_assessment, preprocessor)
    else:
        logging.error("No valid articles found or API returned an error.")

if __name__ == "__main__":
    main()
