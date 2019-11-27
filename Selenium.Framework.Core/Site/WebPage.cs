using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Selenium.Framework.Core.DOMElement;
using Selenium.Framework.Core.Driver;
using Selenium.Framework.Core.Logger;
using System;
using System.Collections.Generic;

namespace Selenium.Framework.Core
{
    public class WebPage
    {
        public IWebDriver _driver { get; set; }

        public string Url { get; set; }

        public WebPage()
        {

        }

        public WebPage(Enum type, string url)
        {
            this.Url = url;
            if (type.Equals(Browser.Type.Chrome))
            {
                this._driver = (ChromeDriver)Browser.Open(Browser.Type.Chrome);
                _driver.Navigate().GoToUrl(url);
                ConsoleLogger.Log("Open url: " + url);
            }
        }


        public void EditInputText(string id,string text)
        {
            _driver.FindElement(By.Id(id)).Clear();
            _driver.FindElement(By.Id(id)).SendKeys(text);
            ConsoleLogger.Log("Edit InputText: " + text);
        }

        public void Click(string id)
        {
            _driver.FindElement(By.Id(id)).Click();
            ConsoleLogger.Log("Click: " + _driver.FindElement(By.Id(id)).Text);
        }

        public void ClickById(string id)
        {
            var nav_items_count = _driver.FindElement(By.Id(id)).FindElements(By.CssSelector("li")).Count;

            for (int i = 1; i <= nav_items_count; i++)
            {
                _driver.FindElement(By.Id(id)).FindElement(By.CssSelector("li:nth-child("+i.ToString()+") > a")).Click();
                ConsoleLogger.Log("Click: " + _driver.FindElement(By.Id(id)).FindElement(By.CssSelector("li:nth-child(" + i.ToString() + ")")).Text);
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
            }
            
        }

        public void ClickByXPath(string xpath)
        {
            ConsoleLogger.Log("will click: " + _driver.FindElement(By.XPath(xpath)).GetAttribute("value"));
            _driver.FindElement(By.XPath(xpath)).Click();
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
        }

        public void ClickAlertMessage(string action)
        {
            if (action.Equals("OK"))
            {
                _driver.SwitchTo().Alert().Accept();
            }
        }

    }
}

