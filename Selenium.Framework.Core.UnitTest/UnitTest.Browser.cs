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
        }


        [TestMethod]
        public void OpenChrome()
        {

        }

        [TestMethod]
        public void OpenFireFox()
        {

        }

        [TestMethod]
        public void OpenIE()
        {

        }
    }
}
