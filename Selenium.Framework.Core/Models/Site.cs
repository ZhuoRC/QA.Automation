using OpenQA.Selenium;
using Selenium.Framework.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Selenium.Framework.Core.Models
{
    public class Site 
    {
        public Site()
        {

        }
        
        public Site(Browser.Type browserType, SiteEnvironment env)
        {
            this._driver = Browser.Open(browserType);
            this._env = env;
            _driver.Navigate().GoToUrl(env.Host);
            Thread.Sleep(3000);
        }

        public IWebDriver _driver { get; set; }

        public SiteEnvironment _env { get; set; }

    }
}
