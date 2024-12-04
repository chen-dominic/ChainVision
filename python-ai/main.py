import logging
from flask import Flask, request, jsonify
from src.models.risk_assessment_model import RiskAssessment
from src.data_preprocessing.text_preprocessor import TextPreprocessor
import yaml

# Configure logging
logging.basicConfig(level=logging.INFO)

# Initialize Flask app
app = Flask(__name__)

# Load the YAML configuration
def load_config(filepath="/Users/sbalamoni/ChainVision/python-ai/config/config.yaml"):
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

# Initialize preprocessor for text processing
preprocessor = TextPreprocessor()

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
        cost_increase = risk_assessment.predict_cost_increase(content)

        # Return response
        return jsonify({
            "content": content,
            "risk_score": risk_score,
            "risk_level": risk_level,
            "ingredients": ingredients,
            "cost_increase": cost_increase  # Include predicted cost increase in response
        }), 200

    except Exception as e:
        logging.error(f"Error in risk assessment: {e}")
        return jsonify({"error": str(e)}), 500
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
        cost_increase = risk_assessment.predict_cost_increase(content)

        # Return response
        return jsonify({
            "content": content,
            "risk_score": risk_score,
            "risk_level": risk_level,
            "ingredients": ingredients,
            "cost_increase": cost_increase  # Include predicted cost increase in response
        }), 200

    except Exception as e:
        logging.error(f"Error in risk assessment: {e}")
        return jsonify({"error": str(e)}), 500
    """
    REST API endpoint for assessing risk in articles and predicting cost increase risk.
    """
    try:
        # Parse input JSON
        data = request.get_json()
        if not data or 'content' not in data:
            return jsonify({"error": "Missing 'content' field in request"}), 400
        
        content = data['content']
        
        # Process content with risk assessment model
        risk_score = risk_assessment.assess_article(content)
        risk_level = risk_assessment.get_risk_level(risk_score)
        ingredients = risk_assessment.extract_ingredients(content)

        # Predict cost increase risk (you can modify this logic based on the ML model)
        cost_increase_score = risk_assessment.predict_cost_increase(content)
        cost_increase_level = "High" if cost_increase_score >= 7 else "Low"  # Simplified logic, use ML model

        # Return response with both delay and cost increase predictions
        return jsonify({
            "content": content,
            "risk_score": risk_score,
            "risk_level": risk_level,
            "ingredients": ingredients,
            "cost_increase": {
                "risk_score": cost_increase_score,
                "risk_level": cost_increase_level
            }
        }), 200

    except Exception as e:
        logging.error(f"Error in risk assessment: {e}")
        return jsonify({"error": str(e)}), 500

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5001, debug=True)