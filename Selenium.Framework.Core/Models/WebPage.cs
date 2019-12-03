using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.PageObjects;

namespace Selenium.Framework.Core
{
    public class WebPage
    {
        public IWebDriver _driver { get; set; }

        public string RelativeUrl { get; set; }

        public WebPage()
        {
        }

        public WebPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void ClickAlertMessage(string action)
        {
            if (action.Equals("OK"))
            {
                _driver.SwitchTo().Alert().Accept();
            }
        }

        public void MoveToElement(IWebElement webElement)
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(webElement).Perform();
        }

        public void JSClick(IWebElement webElement)
        {
            ((IJavaScriptExecutor) _driver).ExecuteScript("arguments[0].click();", webElement);
        }


        public void WaitForSendKey(IWebElement webElement, string input, int timeoutSecond = 10)
        {
            new WebDriverWait(_driver, new TimeSpan(0, 0, timeoutSecond))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(webElement))
                .SendKeys(input);

        }

        public void WaitForClick(IWebElement webElement, int timeoutSecond = 10)
        {

            this.MoveToElement(webElement);

            new WebDriverWait(_driver, new TimeSpan(0, 0, timeoutSecond))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(webElement))
                .Click();
        }

        public void WaitForClickById(IWebElement webElement, int timeoutSecond = 10)
        {
            this.MoveToElement(webElement);

            By by = By.Id(webElement.GetAttribute("id"));

            new WebDriverWait(_driver, new TimeSpan(0, 0, timeoutSecond))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by))
                .Click();
        }

        public void WaitFor(IWebElement webElement, int timeoutSecond = 10)
        {
            new WebDriverWait(_driver, new TimeSpan(0, 0, timeoutSecond))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(webElement));
        }

        public void WaitForById(IWebElement webElement, int timeoutSecond = 10)
        {
            By by = By.Id(webElement.GetAttribute("id"));
            new WebDriverWait(_driver, new TimeSpan(0, 0, timeoutSecond))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
        }
    }
}

