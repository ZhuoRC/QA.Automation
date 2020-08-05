using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Selenium.Framework.Core.Validation
{
    public class Assertion : IAssertion
    {

        IWebDriver _driver { get; set; }
        ILogger _logger { get; set; }

        public Assertion(IWebDriver driver, ILogger logger)
        {
            _driver = driver;
            _logger = logger;
        }


        #region Exist


        public void ExistElement(IWebElement webElement, string failMessage="")
        {
            try
            {
                Assert.IsTrue(webElement.Displayed, failMessage);

            }
            catch (Exception)
            {
                Assert.Fail("webElement not exist");
            }
        }
        public void ExistText(string text, string failMessage)
        {
            try
            {
                _driver.FindElement(By.XPath("//*[text()='" + text + "']"));
                _logger.Log($"{text} exist!");
            }
            catch (Exception)
            {
                if (failMessage == null)
                {
                    Assert.Fail($"{text} not exist");

                }
                else
                {
                    Assert.Fail(failMessage);

                }
            }
            finally
            {
                _logger.TakeSnapshot(_driver, $"EXIST_TEXT_{text}");
            }
        }


        public void NotExistText(string text, string failMessage)
        {
            try
            {
                _driver.FindElement(By.XPath("//*[text()='" + text + "']"));
                Assert.Fail(failMessage);
            }
            catch (Exception)
            {

            }
            finally
            {
                _logger.TakeSnapshot(_driver, $"NOT_EXIST_TEXT_{text}");
            }

        }
        #endregion

        #region Match


        public void MatchText(IWebElement webElement, string text, string failMessage)
        {
            try
            {
                Thread.Sleep(2000);

                this.ExistElement(webElement);


                Assert.IsTrue(text == webElement.Text, failMessage);
            }
            catch (Exception e)
            {
                Assert.Fail($"Expect: {text}, Actual: {webElement.Text}. Exception: {e.Message}");
            }
            finally
            {
                _logger.TakeSnapshot(_driver, $"EXACT_TEXT_{text}");
            }
        }

        public void MatchValue(IWebElement webElement, string value, string failMessage)
        {
            try
            {
                Thread.Sleep(2000);

                this.ExistElement(webElement);

                Assert.IsTrue(value == webElement.GetAttribute("value"), failMessage);
            }
            catch (Exception e)
            {
                Assert.Fail($"Expect: {value}, Actual: {webElement.Text}. Exception: {e.Message}");
            }
            finally
            {
                _logger.TakeSnapshot(_driver, $"EXACT_TEXT_{value}");
            }
        }
        #endregion

        #region Selected

        public void SelectedOption(IWebElement webElement, string text, string failMessage)
        {
            SelectElement select = new SelectElement(webElement);
            string selectedOption = select.SelectedOption.Text;

            Assert.IsTrue(selectedOption == text, failMessage); 
        }
        #endregion
    }
}
