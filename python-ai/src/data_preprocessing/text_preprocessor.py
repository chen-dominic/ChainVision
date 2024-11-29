import string

class TextPreprocessor:
    """
    Handles text cleaning and preprocessing.
    """

    @staticmethod
    def preprocess(content):
        """
        Removes punctuation and converts content to lowercase.
        """
        return content.translate(str.maketrans("", "", string.punctuation)).lower()
