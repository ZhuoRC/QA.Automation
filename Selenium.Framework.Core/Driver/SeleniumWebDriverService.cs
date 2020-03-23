using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Selenium.Framework.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Selenium.Framework.Core
{
    static public class SeleniumWebDriverService
    {
        static public IWebDriver Open(Browser browser)
        {

            if (browser.Type.Equals(BrowserType.Chrome))
            {
                return OpenChrome(browser);
            }

            return null;
        }

        static public IWebDriver OpenChrome(Browser browser)
        {

            

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            //options.AddArgument("--headless");

            if (browser.BrowserExtension != null)
            {
                ChromeExtension ext = (ChromeExtension)browser.BrowserExtension;

                options.AddArgument($"--app-ip={ext.ExtensionId}");
                if (File.Exists(ext.ExtensionLocalPath))
                {
                    options.AddExtension(ext.ExtensionLocalPath);
                }

            }
            else
            {
                
            }
            IWebDriver driver = new ChromeDriver(browser.SeleniumWebDriverPath, options);
            return driver;
        }

        static public void Close(IWebDriver driver)
        {
            Thread.Sleep(2000);
            driver.Quit();
        }
    }
}
