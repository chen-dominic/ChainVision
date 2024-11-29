import spacy

class RiskAssessment:
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
