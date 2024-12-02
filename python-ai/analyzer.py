import spacy

# Load spaCy model
nlp = spacy.load("en_core_web_sm")

def calculate_risk_score(content):
    # Simple risk scoring based on keywords
    risk_score = 0
    risk_keywords = ['ban', 'shortage', 'disruption', 'crisis', 'failure', 'market', 'volatility']
    
    if content:
        for keyword in risk_keywords:
            if keyword in content.lower():
                risk_score += 2  # Increase score for each keyword found
    return risk_score

def analyze_risk_level(risk_score):
    # Return risk level based on score
    if risk_score >= 6:
        return "Critical"
    elif risk_score >= 4:
        return "High"
    elif risk_score >= 2:
        return "Medium"
    else:
        return "Low"

def extract_ingredients(content):
    # Using spaCy to extract entities (like ingredients or relevant terms)
    doc = nlp(content)
    ingredients = [ent.text for ent in doc.ents if ent.label_ == 'PRODUCT']
    return ingredients
