using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace CreditCards.UITests
{
    [TestClass]
    public class CreditCardWebAppShould
    {
        const string homeUrl = "http://localhost:44108/";
        const string homeAboutUrl = "http://localhost:44108/Home/About";
        const string contactUrl = "http://localhost:44108/Home/Contact";

        const string homeTitle = "Home Page - Credit Cards";

        [TestMethod]
        public void LoadApplicationPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);

                DemoHelper.Pause();

                Assert.AreEqual(homeTitle, driver.Title);
                Assert.AreEqual(homeUrl, driver.Url);
            }
        }

        [TestMethod]
        public void ReloadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);

                DemoHelper.Pause();

                driver.Navigate().Refresh();

                Assert.AreEqual(homeTitle, driver.Title);
                Assert.AreEqual(homeUrl, driver.Url);
            }
        }

        [TestMethod]
        public void ReloadHomePageOnBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                IWebElement generationTokenElement = driver.FindElement(By.Id("GenerationToken"));
                string initialToken = generationTokenElement.Text;
                DemoHelper.Pause();

                driver.Navigate().GoToUrl(homeAboutUrl);
                DemoHelper.Pause();

                driver.Navigate().Back();
                DemoHelper.Pause();
                generationTokenElement = driver.FindElement(By.Id("GenerationToken"));
                string reloadToken = generationTokenElement.Text;
                Assert.AreEqual(homeTitle, driver.Title);
                Assert.AreEqual(homeUrl, driver.Url);
                Assert.AreNotEqual(initialToken, reloadToken);
            }
        }

        [TestMethod]
        public void ReloadHomePageOnForward()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeAboutUrl);
                DemoHelper.Pause();

                driver.Navigate().GoToUrl(homeUrl);
                DemoHelper.Pause();

                driver.Navigate().Back();
                DemoHelper.Pause();

                driver.Navigate().Forward();
                DemoHelper.Pause();

                Assert.AreEqual(homeTitle, driver.Title);
                Assert.AreEqual(homeUrl, driver.Url);
            }
        }

        [TestMethod]
        public void DisplayProductsAndRates()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                DemoHelper.Pause();

                ReadOnlyCollection<IWebElement> tableCells = driver.FindElements(By.TagName("td"));
               
                Assert.AreEqual(tableCells[0].Text, "Easy Credit Card");
                Assert.AreEqual(tableCells[1].Text, "20% APR");
            }
        }      
        
        [TestMethod]
        public void OpenContactFooterInNewTab()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                driver.FindElement(By.Id("ContactFooter")).Click();
                DemoHelper.Pause();
                ReadOnlyCollection<string> allTabs = driver.WindowHandles;
                string homeTab = allTabs[0];
                string contactTab = allTabs[1];
                driver.SwitchTo().Window(contactTab);
                DemoHelper.Pause();
                Assert.AreEqual(contactUrl, driver.Url);
            }
        }
    }
}
