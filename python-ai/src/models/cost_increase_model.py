import pickle
from sklearn.ensemble import RandomForestClassifier
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.pipeline import make_pipeline

# Sample training function (you need to have training data for this to work)
def train_cost_increase_model():
    # Your dataset here (you need a dataset for cost increase)
    articles = ["article about cost increase", "another article about tariffs", "more data about price hikes"]
    labels = [1, 1, 0]  # 1 means cost increase, 0 means no cost increase

    # Create a pipeline with a TfidfVectorizer and RandomForestClassifier
    model = make_pipeline(TfidfVectorizer(), RandomForestClassifier())
    model.fit(articles, labels)

    # Save the model
    with open("cost_increase_model.pkl", "wb") as f:
        pickle.dump(model, f)

# If needed, load the model later
def load_cost_increase_model():
    with open("cost_increase_model.pkl", "rb") as f:
        return pickle.load(f)

