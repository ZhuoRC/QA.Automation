using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using Selenium.Framework.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Selenium.Framework.Core
{
    public static class Browser
    {
        public enum Type {Chrome, Firefox ,IE};

        public static IWebDriver Open(Enum type)
        {
            dynamic objType = (new Site()).GetType();
            string driverPath = Path.GetDirectoryName(objType.Assembly.Location)+ "\\Driver";
            Console.WriteLine("selenium driver path: "+driverPath);
            if (type.Equals(Type.Chrome))
            { 
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--start-maximized");
                //options.AddArgument("--disable-infobars");
                options.AddArgument("disable-infobars");
                IWebDriver driver = new ChromeDriver(driverPath, options);
                return driver;
            }
            if (type.Equals(Type.Firefox))
            {
                IWebDriver driver = new FirefoxDriver(driverPath);
                return driver;
            }
            if (type.Equals(Type.IE))
            {
                IWebDriver driver = new InternetExplorerDriver(driverPath);
                return driver;
            }

            return null;

        }

        public static void Close(IWebDriver driver)
        {
            Thread.Sleep(3000);
            driver.Quit();
        }

        public static void Close(Site site)
        {
            Thread.Sleep(3000);
            site._driver.Quit();
        }

        public static void Close(WebPage webPage)
        {
            Thread.Sleep(3000);
            webPage._driver.Quit();
        }
    }
}
