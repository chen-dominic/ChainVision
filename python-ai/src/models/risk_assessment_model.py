import spacy

class RiskAssessment:
    def __init__(self, risk_keywords, critical_ingredients, model=None):
        self.risk_keywords = risk_keywords
        self.critical_ingredients = critical_ingredients
        self.model = model  # Optional: Add ML model for cost increase prediction

    def assess_article(self, content):
        # Existing code to assess article risk (as you've already implemented)
        risk_score = 0
        # Risk assessment logic here (for example based on keywords)
        return risk_score

    def get_risk_level(self, score):
        # Existing code for determining risk level
        if score >= 6:
            return "Critical"
        elif score >= 4:
            return "High"
        elif score >= 2:
            return "Medium"
        else:
            return "Low"

    def extract_ingredients(self, content):
        # Existing code to extract ingredients
        ingredients = []
        return ingredients

    def predict_cost_increase(self, content):
        """
        Predict the cost increase based on the article's content and historical data.
        This method will return a predicted cost increase factor (could be a percentage).
        """
        # You can integrate machine learning here for predictions.
        # If you have historical data or trained models, apply the model to the article content
        # Here we'll just simulate it with a simple condition

        cost_increase = 0  # Default: no cost increase

        # Basic example logic to simulate cost increase prediction based on content:
        if "shortage" in content.lower() or "delay" in content.lower():
            cost_increase = 5  # Predicted cost increase is 5% for shortages and delays
        elif "tariff" in content.lower():
            cost_increase = 10  # Predicted cost increase is 10% for tariffs
        # You can add more conditions based on patterns identified in historical data

        # Return the predicted cost increase as a percentage
        return cost_increase
    def __init__(self, risk_keywords, critical_ingredients):
        self.risk_keywords = risk_keywords
        self.critical_ingredients = critical_ingredients
        self.nlp = spacy.load("en_core_web_sm")

    def assess_article(self, content):
        """
        Calculate the risk score for an article based on keywords.
        """
        risk_score = 0
        for keyword, weight in self.risk_keywords.items():
            if keyword in content.lower():
                risk_score += weight
        return risk_score

    def get_risk_level(self, score):
        """
        Determine the risk level based on the risk score.
        """
        if score >= 10:
            return "Critical"
        elif score >= 7:
            return "High"
        elif score >= 4:
            return "Medium"
        elif score >= 2:
            return "Low"
        else:
            return "Very Low"

    def extract_ingredients(self, content):
        ingredients = []
        for ingredient, data in self.critical_ingredients.items():
            for keyword in data['keywords']:
                if keyword.lower() in content.lower():
                    ingredients.append(ingredient)
        return ingredients
        
        doc = self.nlp(content)
        ingredients_found = []
        for ent in doc.ents:
            if ent.label_ == "PRODUCT" and ent.text.lower() in self.critical_ingredients:
                ingredients_found.append(ent.text)
        return ingredients_found
