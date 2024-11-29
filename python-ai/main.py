import logging
import yaml
import requests
from flask import Flask, request, jsonify
from src.models.risk_assessment_model import RiskAssessment
from src.data_preprocessing.text_preprocessor import TextPreprocessor

# Configure logging
logging.basicConfig(level=logging.INFO)

# Initialize Flask app
app = Flask(__name__)

# Load the YAML configuration
def load_config(filepath="/Users/sbalamoni/ChainVision/python-ai/config/config.yaml"):
    """
    Load configuration from a YAML file.
    """
    try:
        with open(filepath, "r") as file:
            return yaml.safe_load(file)
    except Exception as e:
        logging.error(f"Error loading config file: {e}")
        return {}

# Initialize RiskAssessment with configuration
config = load_config()
risk_assessment = RiskAssessment(
    risk_keywords=config.get("risk_keywords", {}),
    critical_ingredients=config.get("critical_ingredients", {})
)
preprocessor = TextPreprocessor()

# Fetching news articles from the API
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
    try:
        title = article.get("title", "No Title Available")
        content = preprocessor.preprocess(article.get("content", "No Content Available"))
        logging.info(f"Processing article: {title}")
        logging.info(f"Content: {content}")

        # Risk analysis
        risk_score = risk_assessment.assess_article(content)
        risk_level = risk_assessment.get_risk_level(risk_score)

        # Extract ingredients
        ingredients = risk_assessment.extract_ingredients(content)
        ingredients = ", ".join(ingredients) if ingredients else "None"
        logging.info(f"Ingredients found: {ingredients}")

        # Log article details
        logging.info(f"Title: {title}")
        logging.info(f"Link: {article.get('link', 'No Link')}")
        logging.info(f"Risk Level: {risk_level}")
        logging.info(f"Risk Score: {risk_score}")
        logging.info(f"Ingredients: {ingredients}")
        logging.info("-" * 50)

        return {
            "title": title,
            "link": article.get("link", "No Link"),
            "risk_level": risk_level,
            "risk_score": risk_score,
            "ingredients": ingredients
        }

    except Exception as e:
        logging.error(f"Error processing article: {e}")
        return None
    """
    Processes a single article and returns the result.
    """
    try:
        title = article.get("title", "No Title Available")
        content = preprocessor.preprocess(article.get("content", "No Content Available"))
        logging.info(f"Processing article: {title}")
        logging.info(f"Content: {content}")

        # Risk analysis
        risk_score = risk_assessment.assess_article(content)
        risk_level = risk_assessment.get_risk_level(risk_score)

        # Extract ingredients
        ingredients = risk_assessment.extract_ingredients(content)
        ingredients = ", ".join(ingredients) if ingredients else "None"
        logging.info(f"Ingredients found: {ingredients}")

        return {
            "title": title,
            "link": article.get("link", "No Link"),
            "risk_score": risk_score,
            "risk_level": risk_level,
            "ingredients": ingredients
        }

    except Exception as e:
        logging.error(f"Error processing article: {e}")
        return None


@app.route('/api/v1/risk-assessment', methods=['POST'])
def risk_assessment_endpoint():
    """
    REST API endpoint for assessing risk in articles.
    """
    try:
        # Parse input JSON
        data = request.get_json()
        if not data or 'content' not in data:
            return jsonify({"error": "Missing 'content' field in request"}), 400
        
        content = data['content']
        # Process content
        risk_score = risk_assessment.assess_article(content)
        risk_level = risk_assessment.get_risk_level(risk_score)
        ingredients = risk_assessment.extract_ingredients(content)

        # Return response
        return jsonify({
            "content": content,
            "risk_score": risk_score,
            "risk_level": risk_level,
            "ingredients": ingredients
        }), 200

    except Exception as e:
        logging.error(f"Error in risk assessment: {e}")
        return jsonify({"error": str(e)}), 500

@app.route('/api/v1/news', methods=['POST'])
def batch_process_news():
    """
    Batch processing endpoint for multiple articles.
    """
    try:
        # Parse input JSON
        data = request.get_json()
        if not data or 'articles' not in data:
            return jsonify({"error": "Missing 'articles' field in request"}), 400
        
        articles = data['articles']
        results = []

        for article in articles:
            result = process_article(article, risk_assessment, preprocessor)
            if result:
                results.append(result)

        return jsonify({"status": "success", "results": results}), 200

    except Exception as e:
        logging.error(f"Error in batch processing: {e}")
        return jsonify({"error": str(e)}), 500

if __name__ == '__main__':
    app.run(debug=True, port=5001)  # You can specify a different port if 5000 is in use
