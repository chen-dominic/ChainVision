import string

class TextPreprocessor:
    def __init__(self):
        pass

    def preprocess(self, article_content):
        cleaned_text = article_content.lower()  # Convert to lowercase
        cleaned_text = ''.join([char for char in cleaned_text if char.isalnum() or char.isspace()])  # Remove punctuation
        return cleaned_text
