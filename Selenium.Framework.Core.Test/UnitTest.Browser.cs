using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using Selenium.Framework.Core.Driver;

namespace Selenium.Framework.Core.Test
{
    [TestClass]
    public class UnitTestBrowser
    {
        [TestMethod]
        public void OpenChrome()
        {
            ChromeDriver driver = (ChromeDriver)Browser.Open(Browser.Type.Chrome);
            driver.Navigate().GoToUrl("https://www.google.com");
            driver.Quit();
        }
    }
}
