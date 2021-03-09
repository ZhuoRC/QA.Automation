using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Opera;
using System.IO;
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

            if (browser.Type.Equals(BrowserType.Opera))
            {
                return OpenOpera(browser);
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

        static public IWebDriver OpenOpera(Browser browser)
        {

            OperaOptions options = new OperaOptions();
            options.AddArgument("--start-maximized");

            options.BinaryLocation = $"{browser.SeleniumWebDriverPath}\\operadriver.exe";
            options.BinaryLocation = @"C:\Opera\launcher.exe";
            options.BinaryLocation = @"C:\Opera\73.0.3856.344\opera.exe";
            IWebDriver driver = new OperaDriver(options);


            //OperaOptions options = new OperaOptions();
            //options.AddArgument("--start-maximized");

            //IWebDriver driver = new OperaDriver(browser.SeleniumWebDriverPath, options);
            return driver;
        }

        static public void Close(IWebDriver driver)
        {
            Thread.Sleep(2000);
            driver.Quit();
        }
    }
}
