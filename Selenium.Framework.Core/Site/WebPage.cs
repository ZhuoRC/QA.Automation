using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Selenium.Framework.Core.DOMElement;
using Selenium.Framework.Core.Driver;
using System;
using System.Collections.Generic;

namespace Selenium.Framework.Core
{
    public class WebPage
    {
        public IWebDriver driver { get; set; }

        public string Url { get; set; }

        public WebPage()
        {

        }

        public WebPage(Enum type, string url)
        {
            this.Url = url;
            if (type.Equals(Browser.Type.Chrome))
            {
                this.driver = (ChromeDriver)Browser.Open(Browser.Type.Chrome);
                driver.Navigate().GoToUrl(url);
            }
        }


        public void EditInputText(string id,string text)
        {
            driver.FindElement(By.Id(id)).Clear();
            driver.FindElement(By.Id(id)).SendKeys(text);
        }

        public void Click(string id)
        {
            driver.FindElement(By.Id(id)).Click();
        }

        public void Clicks(string id)
        {
            var nav_items_count = driver.FindElement(By.Id(id)).FindElements(By.CssSelector("li")).Count;

            for (int i = 1; i <= nav_items_count; i++)
            {
                driver.FindElement(By.Id(id)).FindElement(By.CssSelector("li:nth-child("+i.ToString()+") > a")).Click();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            }
            
        }

        public void ClickAlertMessage(string action)
        {
            if (action.Equals("OK"))
            {
                driver.SwitchTo().Alert().Accept();
            }
        }

    }
}

