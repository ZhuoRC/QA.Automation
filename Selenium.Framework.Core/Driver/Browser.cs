using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Selenium.Framework.Core.Driver
{
    public static class Browser
    {
        public enum Type {Chrome, Firefox ,IE};

        public static object Open(Enum type)
        {
            if (type.Equals(Type.Chrome))
            {
                ChromeDriver driver = new ChromeDriver(@".\Driver\");
                return driver;
            }
            if (type.Equals(Type.Firefox))
            {
                FirefoxDriver driver = new FirefoxDriver(@".\Driver\");
                return driver;
            }
            if (type.Equals(Type.IE))
            {
                InternetExplorerDriver driver = new InternetExplorerDriver(@".\Driver\");
                return driver;
            }

            return null;

        }

        public static void Close(IWebDriver driver)
        {
            Thread.Sleep(3000);
            driver.Quit();
        }

        public static void Close(WebPage webPage)
        {
            Thread.Sleep(3000);
            webPage.driver.Quit();
        }
    }
}
