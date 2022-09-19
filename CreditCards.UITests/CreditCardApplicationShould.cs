using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace CreditCards.UITests
{
    [TestClass]
    public class CreditCardApplicationShould
    {
        const string homeUrl = "http://localhost:44108/";
        const string creditApplyUrl = "http://localhost:44108/Apply";

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

                IWebElement carouselNext = driver.FindElement(By.CssSelector("[data-slide='next']"));
                carouselNext.Click();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                IWebElement applyLink = wait.Until((d) => d.FindElement(By.LinkText("Easy: Apply Now!")));

                applyLink.Click();

                DemoHelper.Pause();

                Assert.AreEqual(creditCardTitle, driver.Title);
                Assert.AreEqual(creditApplyUrl, driver.Url);
            }
        }
        [TestMethod]
        public void BeInitiatedFromHomePage_CustomerService()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);

                IWebElement carouselNext = driver.FindElement(By.CssSelector("[data-slide='next']"));
                carouselNext.Click();
                DemoHelper.Pause(1000);

                carouselNext.Click();
                DemoHelper.Pause(1000);

                IWebElement applyLink = driver.FindElement(By.ClassName("customer-service-apply-now"));
                applyLink.Click();

                DemoHelper.Pause();

                Assert.AreEqual(creditCardTitle, driver.Title);
                Assert.AreEqual(creditApplyUrl, driver.Url);
            }
        }

        [TestMethod]
        public void BeInitiatedFromHomePage_RandomGreeting()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                DemoHelper.Pause();

                IWebElement randomGreetingApplyLink = driver.FindElement(By.PartialLinkText("- Apply Now!"));
                randomGreetingApplyLink.Click();

                DemoHelper.Pause();

                Assert.AreEqual(driver.Url, creditApplyUrl);
                Assert.AreEqual(driver.Title, creditCardTitle);
            }
        }

        [TestMethod]
        public void BeInitiatedFromHomePage_RandomGreeting_Using_XPATH()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                DemoHelper.Pause();

                IWebElement randomGreetingApplyLink = driver.FindElement(By.XPath("//a[text()[contains(.,'- Apply Now!')]]"));
                randomGreetingApplyLink.Click();

                DemoHelper.Pause();

                Assert.AreEqual(driver.Url, creditApplyUrl);
                Assert.AreEqual(driver.Title, creditCardTitle);
            }
        }

        [TestMethod]
        public void BeSubmittedWhenValid()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(creditApplyUrl);

                driver.FindElement(By.Id("FirstName")).SendKeys("Luna");
                DemoHelper.Pause(1000);

                driver.FindElement(By.Id("LastName")).SendKeys("Lovegood");
                DemoHelper.Pause(1000);

                driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys("123456-A");
                DemoHelper.Pause(1000);

                driver.FindElement(By.Id("Age")).SendKeys("18");
                DemoHelper.Pause(1000);

                driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys("50000");
                DemoHelper.Pause(1000);

                driver.FindElement(By.Id("Single")).Click();
                DemoHelper.Pause(1000);

                IWebElement businessSourceSelectElement = driver.FindElement(By.Id("BusinessSource"));
                SelectElement businessSource = new SelectElement(businessSourceSelectElement);
                Assert.AreEqual(businessSource.SelectedOption.Text, "I'd Rather Not Say");
                foreach(IWebElement option in businessSource.Options)
                {
                    Console.WriteLine($"Value: {option.GetAttribute("value")} Text: {option.Text}");
                }

                Assert.AreEqual(5, businessSource.Options.Count);
                businessSource.SelectByValue("Email");
                DemoHelper.Pause(1000);
                businessSource.SelectByText("Internet Search");
                DemoHelper.Pause(1000);
                businessSource.SelectByIndex(4);
                DemoHelper.Pause(1000);

                driver.FindElement(By.Id("TermsAccepted")).Click();
                DemoHelper.Pause(1000);

                // driver.FindElement(By.Id("SubmitApplication")).Click();
                driver.FindElement(By.Id("Single")).Submit();
                DemoHelper.Pause(1000);

                Assert.AreEqual("Application Complete - Credit Cards", driver.Title);
                Assert.AreEqual("ReferredToHuman", driver.FindElement(By.Id("Decision")).Text);
                Assert.AreEqual("Luna Lovegood", driver.FindElement(By.Id("FullName")).Text);
                Assert.AreNotEqual("", driver.FindElement(By.Id("ReferenceNumber")).Text);
                Assert.AreEqual("18", driver.FindElement(By.Id("Age")).Text);
                Assert.AreEqual("50000", driver.FindElement(By.Id("Income")).Text);
                Assert.AreEqual("TV", driver.FindElement(By.Id("BusinessSource")).Text);
            }
        }

        [TestMethod]
        public void BeSubmittedWhenValidationErrorsCorrected()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(creditApplyUrl);

                driver.FindElement(By.Id("FirstName")).SendKeys("Luna");
                DemoHelper.Pause(1000);

                // Don't enter last name

                driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys("123456-A");
                DemoHelper.Pause(1000);

                driver.FindElement(By.Id("Age")).SendKeys("16");
                DemoHelper.Pause(1000);

                driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys("50000");
                DemoHelper.Pause(1000);

                driver.FindElement(By.Id("Single")).Click();
                DemoHelper.Pause(1000);

                IWebElement businessSourceSelectElement = driver.FindElement(By.Id("BusinessSource"));
                SelectElement businessSource = new SelectElement(businessSourceSelectElement);
                
                businessSource.SelectByIndex(4);
                DemoHelper.Pause(1000);

                driver.FindElement(By.Id("TermsAccepted")).Click();
                DemoHelper.Pause(1000);

                driver.FindElement(By.Id("SubmitApplication")).Click();
                DemoHelper.Pause(1000);

                // Assert validations failed
                var validationErrors = driver.FindElements(By.CssSelector(".validation-summary-errors > ul > li"));
                Assert.AreEqual(2, validationErrors.Count);
                Assert.AreEqual("Please provide a last name", validationErrors[0].Text);
                Assert.AreEqual("You must be at least 18 years old", validationErrors[1].Text);

                // Fix errors
                driver.FindElement(By.Id("Age")).Clear();
                driver.FindElement(By.Id("Age")).SendKeys("18");
                DemoHelper.Pause(1000);
                driver.FindElement(By.Id("LastName")).SendKeys("Lovegood");
                DemoHelper.Pause(1000);

                // Resubmit form
                driver.FindElement(By.Id("SubmitApplication")).Click();
                DemoHelper.Pause(1000);
                Assert.AreEqual("Application Complete - Credit Cards", driver.Title);
                Assert.AreEqual("ReferredToHuman", driver.FindElement(By.Id("Decision")).Text);
                Assert.AreEqual("Luna Lovegood", driver.FindElement(By.Id("FullName")).Text);
                Assert.AreNotEqual("", driver.FindElement(By.Id("ReferenceNumber")).Text);
                Assert.AreEqual("18", driver.FindElement(By.Id("Age")).Text);
                Assert.AreEqual("50000", driver.FindElement(By.Id("Income")).Text);
                Assert.AreEqual("TV", driver.FindElement(By.Id("BusinessSource")).Text);
            }
        }
    }
}
