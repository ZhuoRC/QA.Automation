using NUnit.Framework;
using Selenium.Framework.Core;
using Selenium.Framework.Core.Models;
using System;

namespace Selenium.Sample
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_OpenGoolge()
        {
            Site google = new Site(Browser.Type.Chrome, new SiteEnvironment("http://www.google.com"));
            Browser.Close(google);
        }

        [TearDown]
        public void TearDown()
        {
        }
    }
}