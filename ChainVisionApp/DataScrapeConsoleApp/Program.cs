// See https://aka.ms/new-console-template for more information
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Threading;

namespace SeleniumConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); // Timeout set for an implicit wait
            try
            {
                string[] list = new string[]{"SBH25","","",""};

                // Go to the webpage
                driver.Navigate().GoToUrl("https://www.barchart.com/futures/quotes/SBH25/futures-prices");
                Console.WriteLine("Navigating to the webpage for " + list[0]);
                Debug.WriteLine("Navigating to the webpage for " + list[0]);

                Thread.Sleep(4000);

                


            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Debug.WriteLine("ERROR Occured: " + ex.Message);
            }
            finally
            {
                // Close the browser
                driver.Quit();
            }
        }
    }
}
