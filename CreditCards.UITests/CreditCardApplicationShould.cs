using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CreditCards.UITests
{
    [TestClass]
    public class CreditCardApplicationShould
    {
        const string homeUrl = "http://localhost:44108/";
        const string homeAboutUrl = "http://localhost:44108/Home/About";
        const string creditApplyUrl = "http://localhost:44108/Apply";

        const string homeTitle = "Home Page - Credit Cards";
        const string creditCardTitle = "Credit Card Application - Credit Cards";

        [TestMethod]
        public void BeInitiatedFromHomePage_NewLowRate()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);

                DemoHelper.Pause();
                IWebElement applyLink = driver.FindElement(By.Name("ApplyLowRate"));
                applyLink.Click();

                DemoHelper.Pause();

                Assert.AreEqual(creditCardTitle, driver.Title);
                Assert.AreEqual(creditApplyUrl, driver.Url);
            }
        }

        [TestMethod]
        public void BeInitiatedFromHomePage_EasyApplication()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);

                DemoHelper.Pause(11000);
                IWebElement applyLink = driver.FindElement(By.LinkText("Easy: Apply Now!"));
                applyLink.Click();

                DemoHelper.Pause();

                Assert.AreEqual(creditCardTitle, driver.Title);
                Assert.AreEqual(creditApplyUrl, driver.Url);
            }
        }
    }
}
