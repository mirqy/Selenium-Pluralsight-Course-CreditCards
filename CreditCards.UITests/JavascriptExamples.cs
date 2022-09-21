using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CreditCards.UITests
{
    [TestClass]
    public class JavascriptExamples
    {
        [TestMethod]
        public void ClickOverlayedLink()
        {
            using(IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:44108/jsoverlay.html");
                DemoHelper.Pause();
                string script = "document.getElementById('HiddenLink').click()";
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript(script);
                Assert.AreEqual("https://www.pluralsight.com/", driver.Url);
            }
        }

        [TestMethod]
        public void GetOverlayedLinkText()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:44108/jsoverlay.html");
                DemoHelper.Pause();
                string script = "return document.getElementById('HiddenLink').innerHTML;";
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                string linkText = (string)js.ExecuteScript(script);
                Assert.AreEqual("Go to Pluralsight", linkText);
            }
        }
    }
}
