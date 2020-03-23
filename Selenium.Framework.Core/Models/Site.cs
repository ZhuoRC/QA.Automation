using OpenQA.Selenium;
using Selenium.Framework.Core.Models;
using Selenium.Framework.Core.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Selenium.Framework.Core.Models
{
    public class Site
    {
        public IWebDriver _driver { get; set; }

        public SiteEnvironment _env { get; set; }

        public ILogger _logger { get; set; }

        public IAssertion _assert { get; set; }

        public Site()
        {

        }

        public Site(IWebDriver driver, ILogger logger)
        {
            this._driver = driver;
            this._logger = logger;
            this._assert = new Assertion(driver, logger);
        }

        public Site(Browser browser, SiteEnvironment env, ILogger logger)
        {
            this._driver = SeleniumWebDriverService.Open(browser);
            this._env = env;
            this._logger = logger;
            this._assert = new Assertion(this._driver, logger);
        }

        public void Close()
        {
            try
            {
                foreach (string windowHandle in _driver.WindowHandles)
                {
                    Thread.Sleep(2000);
                    SwitchToWindow(windowHandle);
                    _logger.TakeSnapshot(_driver, $"Close_Window_{windowHandle}");
                    _driver.Close();
                }
            }
            catch (Exception ex)
            {
                if (ex is OpenQA.Selenium.WebDriverException)
                {
                    return;
                }
                throw;
            }
        }
        public void RedirectTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
            Thread.Sleep(2000);
            _logger.Log($"redirect to {url}");
            _logger.TakeSnapshot(_driver, $"REDIRECT_TO_{url}");
        }


        public void NavigateTo(WebPage page)
        {
            string newUrl = _env.Host + page.RelativeUrl;
            _driver.Navigate().GoToUrl(newUrl);
            Thread.Sleep(2000);
            _logger.Log($"navigate to {newUrl}");
            _logger.TakeSnapshot(_driver, $"NAVIGATE_TO_{newUrl}");
        }

        public void SwitchToWindow(string windowHandle)
        {
            try
            {
                _driver.SwitchTo().Window(windowHandle);
                Thread.Sleep(2000);
            }
            catch (Exception)
            {
                
            }
        }

        public void SwitchToNewWindow()
        {
            Thread.Sleep(2000);
            string newWindowHandle = _driver.WindowHandles[_driver.WindowHandles.Count - 1];
            _driver.SwitchTo().Window(newWindowHandle);
        }
    }




}
