using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Framework.Core.DOMElement;
using Selenium.Framework.Core.Driver;
using System;

namespace Selenium.Framework.Core
{
    public class WebPage
    {
        public ChromeDriver driver { get; set; }

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

        public void ClickAlertMessage(string action)
        {
            if (action.Equals("OK"))
            {
                driver.SwitchTo().Alert().Accept();
            }
        }

        public void Quit()
        {
            driver.Quit();
        }
    }
}
