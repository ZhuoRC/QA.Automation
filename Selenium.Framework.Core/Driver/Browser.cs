using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Selenium.Framework.Core.Driver
{
    public static class Browser
    {
        public enum Type {Chrome, Firefox ,IE};

        public static object Open(Enum type)
        {
            dynamic objType = (new WebPage()).GetType();
            string driverPath = Path.GetDirectoryName(objType.Assembly.Location)+ "\\Driver";
            Console.WriteLine("selenium driver path: "+driverPath);
            if (type.Equals(Type.Chrome))
            {
                ChromeDriver driver = new ChromeDriver(driverPath);
                return driver;
            }
            if (type.Equals(Type.Firefox))
            {
                FirefoxDriver driver = new FirefoxDriver(driverPath);
                return driver;
            }
            if (type.Equals(Type.IE))
            {
                InternetExplorerDriver driver = new InternetExplorerDriver(driverPath);
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
            webPage._driver.Quit();
        }
    }
}
