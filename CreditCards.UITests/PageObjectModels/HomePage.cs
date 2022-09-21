﻿using OpenQA.Selenium;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CreditCards.UITests.PageObjectModels
{
    class HomePage
    {
        private readonly IWebDriver Driver;
        private const string PageUrl = "http://localhost:44108/";
        private const string PageTitle = "Home Page - Credit Cards";

        public HomePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(PageUrl);
            EnsurePageLoaded();
        }

        public void EnsurePageLoaded()
        {
            bool hasPageLoaded = (Driver.Url == PageUrl) && (Driver.Title == PageTitle);

            if (!hasPageLoaded)
            {
                throw new System.Exception($"Failed to load page. Page URL = '{Driver.Url}' Page Source = \r\n {Driver.PageSource}");
            }

        }

        public ReadOnlyCollection<(string name, string interest)> Products
        {
            get
            {
                var products = new List<(string name, string interestRate)>();
                var productCells = Driver.FindElements(By.TagName("td"));
                for(int i = 0; i < productCells.Count - 1; i+= 2)
                {
                    string name = productCells[i].Text;
                    string interestRate = productCells[i + 1].Text;
                    products.Add((name, interestRate));
                }
                return products.AsReadOnly();
            }
        }
    }
}
