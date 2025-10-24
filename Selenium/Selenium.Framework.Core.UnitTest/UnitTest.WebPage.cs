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
    public class UnitTestWebPage
    {
        public IWebDriver driver;
        public WebPage webPage;

        [TestInitialize()]
        public void Startup()
        {
           
        }

        [TestCleanup()]
        public void Cleanup()
        {
        }

    }
}
