using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.Framework.Core
{
    public interface ILogger
    {
        void Log(string message);
        void TakeSnapshot(IWebDriver driver, string key);
    }
}
