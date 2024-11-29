import pandas as pd
import yaml
import logging

class SupplyChainDataLoader:
    """
    Handles loading data from CSV, API, or YAML configuration files.
    """

    def load_csv(self, filepath):
        try:
            return pd.read_csv(filepath)
        except FileNotFoundError:
            logging.error(f"CSV file not found: {filepath}")
            return pd.DataFrame()

    def load_config(self, filepath):
        try:
            with open(filepath, "r") as file:
                return yaml.safe_load(file)
        except Exception as e:
            logging.error(f"Failed to load config file: {e}")
            return {}

    def load_api(self, api_url):
        try:
            response = requests.get(api_url)
            response.raise_for_status()
            return response.json().get("results", [])
        except Exception as e:
            logging.error(f"Failed to fetch API data: {e}")
            return []
