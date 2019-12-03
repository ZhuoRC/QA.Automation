using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Selenium.Framework.Core;
using System.Threading;

namespace Selenium.Framework.Core.Test
{
    [TestClass]
    public class UnitTestBrowser
    {

        public IWebDriver driver;

        [TestInitialize()]
        public void Startup()
        {
         
        }

        [TestCleanup()]
        public void Cleanup()
        {
            Browser.Close(driver);
        }


        [TestMethod]
        public void OpenChrome()
        {
            driver = (ChromeDriver)Browser.Open(Browser.Type.Chrome);
            driver.Navigate().GoToUrl("https://www.seleniumhq.org/");
        }

        [TestMethod]
        public void OpenFireFox()
        {
            driver = (FirefoxDriver)Browser.Open(Browser.Type.Firefox);
            driver.Navigate().GoToUrl("https://www.seleniumhq.org/");
        }

        [TestMethod]
        public void OpenIE()
        {
            driver = (InternetExplorerDriver)Browser.Open(Browser.Type.IE);
            driver.Navigate().GoToUrl("https://www.seleniumhq.org/");
        }
    }
}
