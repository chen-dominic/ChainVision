import numpy as np
import pandas as pd
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.decomposition import TruncatedSVD
from typing import List, Dict, Any

class SupplyChainFeatureExtractor:
    def __init__(
        self, 
        max_features: int = 1000, 
        ngram_range: tuple = (1, 2)
    ):
        """
        Initialize feature extractor
        
        :param max_features: Maximum number of features
        :param ngram_range: Range of n-grams to consider
        """
        self.tfidf_vectorizer = TfidfVectorizer(
            max_features=max_features,
            ngram_range=ngram_range,
            stop_words='english'
        )
        
        self.lsa_reducer = TruncatedSVD(
            n_components=100, 
            random_state=42
        )
        
        self.risk_keywords = {
            "tariff": 2,
            "supply chain": 3,
            "shortage": 2,
            "cost": 1
        }

    def extract_tfidf_features(self, texts: List[str]) -> np.ndarray:
        """
        Extract TF-IDF features from texts
        
        :param texts: List of text documents
        :return: TF-IDF feature matrix
        """
        tfidf_matrix = self.tfidf_vectorizer.fit_transform(texts)
        return tfidf_matrix

    def reduce_dimensionality(self, tfidf_matrix: np.ndarray) -> np.ndarray:
        """
        Reduce dimensionality using LSA
        
        :param tfidf_matrix: TF-IDF feature matrix
        :return: Reduced feature matrix
        """
        return self.lsa_reducer.fit_transform(tfidf_matrix)

    def keyword_risk_scoring(self, text: str) -> float:
        """
        Calculate risk score based on keywords
        
        :param text: Input text
        :return: Risk score
        """
        text_lower = text.lower()
        risk_score = sum([
            weight for keyword, weight in self.risk_keywords.items()
            if keyword in text_lower
        ])
        
        return min(risk_score, 1.0)

    def extract_geographic_features(
        self, 
        texts: List[str], 
        countries: List[str]
    ) -> Dict[str, List[float]]:
        """
        Extract geographic risk features
        
        :param texts: List of text documents
        :param countries: List of countries to check
        :return: Geographic risk features
        """
        geo_features = {}
        
        for text in texts:
            text_lower = text.lower()
            country_risks = {
                country: (1.0 if country.lower() in text_lower else 0.0)
                for country in countries
            }
            geo_features[text] = list(country_risks.values())
        
        return geo_features

    def engineer_features(
        self, 
        texts: List[str], 
        countries: List[str]
    ) -> Dict[str, Any]:
        """
        Comprehensive feature engineering
        
        :param texts: List of text documents
        :param countries: List of countries
        :return: Dictionary of engineered features
        """
        # TF-IDF features
        tfidf_matrix = self.extract_tfidf_features(texts)
        lsa_features = self.reduce_dimensionality(tfidf_matrix)
        
        # Risk scoring
        risk_scores = [self.keyword_risk_scoring(text) for text in texts]
        
        # Geographic features
        geo_features = self.extract_geographic_features(texts, countries)
        
        return {
            'tfidf_features': tfidf_matrix.toarray(),
            'lsa_features': lsa_features,
            'risk_scores': risk_scores,
            'geo_features': geo_features
        }
