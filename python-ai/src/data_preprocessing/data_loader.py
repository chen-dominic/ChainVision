import pandas as pd
import re

class SupplyChainDataLoader:
    def __init__(self, data_sources):
        """
        Initialize data loader with multiple data sources.
        :param data_sources: Dictionary with keys as source type (e.g., 'csv', 'api') 
                             and values as file paths or API URLs.
        """
        self.data_sources = data_sources

    def load_data(self, source_type):
        """
        Load data from a specific source (e.g., CSV or API).
        :param source_type: 'csv' or 'api'
        """
        if source_type == "csv":
            return self.load_csv(self.data_sources['csv'])
        elif source_type == "api":
            return self.load_api(self.data_sources['api'])
        else:
            return None

    def load_csv(self, filepath):
        try:
            return pd.read_csv(filepath)
        except FileNotFoundError:
            print(f"CSV file not found: {filepath}")
            return pd.DataFrame()

    def load_api(self, api_url):
        try:
            response = requests.get(api_url)
            response.raise_for_status()
            data = response.json().get("results", [])
            return pd.DataFrame(data)
        except requests.RequestException as e:
            print(f"API request failed: {e}")
            return pd.DataFrame()

