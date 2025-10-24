using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Selenium.Framework.Core.Validation;
using SeleniumExtras.PageObjects;
using System;
using System.Threading;

namespace Selenium.Framework.Core
{
    public class WebPage
    {
        public IWebDriver _driver { get; set; }
        public ILogger _log { get; set; }

        public IAssertion _assert { get; set; }

        public string AbsoluteUrl { get; set; }
        public string RelativeUrl { get; set; }

        public string CurrentWindowHandle { get; set; }

        public WebPage()
        {

        }

        public WebPage(IWebDriver driver, ILogger logger)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
            this.CurrentWindowHandle = driver.CurrentWindowHandle;

            this._log = logger;
            this._assert = new Assertion(driver, logger);
        }

        public void MaximizeWindow()
        {
            _driver.Manage().Window.Maximize();
        }


        #region Indicate

        public void RedirectTo(string url, int waitMs = 2000)
        {
            _driver.Navigate().GoToUrl(url);
            _log.Log($"redirect to {url}");
            Thread.Sleep(waitMs);
        }

        public void SwitchToFrame(IWebElement webElement)
        {
            Thread.Sleep(2000);
            _driver.SwitchTo().Frame(webElement);
        }

        public void SwitchToModel(IWebElement webElement)
        {
            Thread.Sleep(2000);
            _driver.SwitchTo().ActiveElement();
        }

        public void MoveToElement(IWebElement webElement)
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(webElement).Perform();
            Thread.Sleep(200);
        }

        public void JSMoveToElement(IWebElement webElement)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", webElement);
            Thread.Sleep(200);
        }

        public IWebElement GetSiblingWebElement(IWebElement webElement)
        {
            return webElement.FindElement(By.XPath("following-sibling::*"));
        }


        #endregion

        #region Click
        public void ClickAlertMessageAccept()
        {
            _driver.SwitchTo().Alert().Accept();
        }

        public void JSClick(IWebElement webElement)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", webElement);
            Thread.Sleep(200);
        }

        public void SubmitClick(IWebElement webElement, int timeout = 2000)
        {
            string domText = webElement.Text;
            _log.Log($"submit click {domText}");
            _log.TakeSnapshot(_driver, $"SUBMIT_CLICK_{domText}");
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", webElement);
            Thread.Sleep(timeout);
            _log.TakeSnapshot(_driver, $"SUBMIT_COMPLETED_{domText}");
        }
        #endregion

        #region Fill & Select
        public void FillInputField(IWebElement webElement, string input)
        {
            webElement.Clear();
            JSMoveToElement(webElement);
            webElement.SendKeys(input);
        }

        public void JSSendKey(IWebElement webElement, string input)
        {
            JSMoveToElement(webElement);
            webElement.Clear();
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].setAttribute('value',arguments[1]);", webElement, input);
        }

        public void FillDatetimePicker(IWebElement webElement, DateTime date, string format)
        {

            string input = date.ToString(format);

            webElement.Clear();
            webElement.SendKeys(input);
            webElement.SendKeys(Keys.Tab);

        }

        public string GetSelectedOptionText(IWebElement webElement)
        {
            MoveToElement(webElement);
            SelectElement select = new SelectElement(webElement);
            string selectedOption = select.SelectedOption.Text;
            _log.TakeSnapshot(_driver, $"SELECTED_OPTION_{selectedOption}");

            return selectedOption;
        }

        public void SelectOptionByText(IWebElement webElement, string text)
        {
            MoveToElement(webElement);
            SelectElement selectThis = new SelectElement(webElement);
            selectThis.SelectByText(text);
            Thread.Sleep(2000);
            _log.TakeSnapshot(_driver, $"SELECT_OPTION_{text}");
        }
        public void SelectOptionByIndex(IWebElement webElement, int index)
        {
            MoveToElement(webElement);
            SelectElement selectThis = new SelectElement(webElement);
            selectThis.SelectByIndex(index);
            Thread.Sleep(2000);
            _log.TakeSnapshot(_driver, $"SELECT_OPTION_INDEX_{index}");
        }
        #endregion

        #region WaitFor
        public void WaitForFillInputField(IWebElement webElement, string input, int timeoutSecond = 10)
        {
            new WebDriverWait(_driver, new TimeSpan(0, 0, timeoutSecond))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(webElement))
                .Clear();
            webElement.SendKeys(input);

        }
        public void WaitForSendKey(IWebElement webElement, string input, int timeoutSecond = 10)
        {
            new WebDriverWait(_driver, new TimeSpan(0, 0, timeoutSecond))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(webElement))
                .SendKeys(input);
        }

        public void WaitForClick(IWebElement webElement, int timeoutSecond = 10)
        {

            try
            {

                JSClick(
                    new WebDriverWait(_driver, new TimeSpan(0, 0, timeoutSecond))
                    .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(webElement))
                );

                Thread.Sleep(2000);

            }
            catch (Exception)
            {
                _log.TakeSnapshot(_driver, $"WAITFOR_CLICK");
                Assert.Fail($"cannot found {webElement}");
                Thread.Sleep(2000);
            }
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

        public void WaitForText(IWebElement webElement, string text, int timeoutSecond = 10)
        {
            new WebDriverWait(_driver, new TimeSpan(0, 0, timeoutSecond))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(webElement, text));
        }
        #endregion

        #region Check
        public bool SafeCheckWebElement(By by)
        {
            try
            {
                _driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {

                return false;
            }
        }

        public bool ReadOnlyWebElement(IWebElement webElement)
        {
            return webElement.GetAttribute("readonly").Equals("true");
        }

        public bool ExistOptionInSelectElement(IWebElement webElement, string optionText)
        {
            var options = webElement.FindElements(By.TagName("option"));
            foreach (var option in options)
            {
                if (option.Text == optionText)
                {
                    return true;
                }
            }

            return false;
        }


        #endregion

    }
}

