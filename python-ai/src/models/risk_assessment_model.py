import spacy

# Load spaCy model
nlp = spacy.load("en_core_web_sm")

class RiskAssessment:
    def __init__(self, risk_keywords, critical_ingredients, region_weights=None):
        """
        Initializes the risk assessment with keywords, critical ingredients, and optional region-specific weights.
        """
        self.risk_keywords = risk_keywords
        self.critical_ingredients = critical_ingredients
        self.region_weights = region_weights or {}

    def assess_article(self, content, country=None):
        """
        Calculates the risk score for the content based on keywords, context, and country relevance.
        """
        score = 0
        content_lower = content.lower()

        # Keyword-based scoring with context relevance
        for keyword, weight in self.risk_keywords.items():
            if keyword in content_lower:
                # Boost for bakery-critical contexts
                if keyword in ["wheat", "sugar", "flour", "oil", "food"]:
                    weight *= 1.5
                # Deprioritize unrelated contexts like steel
                if "steel" in content_lower and keyword == "tariff":
                    continue
                score += weight

        # Critical ingredient scoring
        for ingredient, data in self.critical_ingredients.items():
            for keyword in data["keywords"]:
                if keyword in content_lower:
                    score += data.get("criticality_score", 1)

        # Region-specific weighting
        if country:
            for region, weight in self.region_weights.items():
                if region.lower() in (country or []):
                    score *= weight

        return max(score, 0.1)  # Ensure a base score

    def get_risk_level(self, score):
        """
        Maps the risk score to a risk level, including Very Low to Critical.
        """
        if score >= 15:
            return "Critical"
        elif score >= 10:
            return "High"
        elif score >= 5:
            return "Medium"
        elif score >= 2:
            return "Low"
        else:
            return "Very Low"

    def extract_ingredients(self, content):
        """
        Extracts ingredient mentions using spaCy's Named Entity Recognition (NER).
        """
        doc = nlp(content)
        return [ent.text for ent in doc.ents if ent.label_ == "PRODUCT"]
