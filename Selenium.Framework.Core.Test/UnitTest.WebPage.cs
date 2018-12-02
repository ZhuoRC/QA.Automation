using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Selenium.Framework.Core.Driver;
using System.Threading;

namespace Selenium.Framework.Core.Test
{
    [TestClass]
    public class UnitTestWebPage
    {
        public IWebDriver driver;
        public WebPage webPage;

        [TestInitialize()]
        public void Startup()
        {
            webPage = new WebPage(Browser.Type.Chrome, @"https://www.seleniumhq.org/");
        }

        [TestCleanup()]
        public void Cleanup()
        {
            Browser.Close(webPage);
        }

        [TestMethod]
        public void EditInputText()
        {
            webPage.EditInputText("q", "input text");
        }

        [TestMethod]
        public void Click()
        {
            webPage.Click("menu_projects");
        }
    }
}
