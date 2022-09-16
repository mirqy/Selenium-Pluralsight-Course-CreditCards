using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CreditCards.UITests
{
    [TestClass]
    public class CreditCardWebAppShould
    {
        const string homeUrl = "http://localhost:44108/";
        const string homeAboutUrl = "http://localhost:44108/Home/About";

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
    }
}
