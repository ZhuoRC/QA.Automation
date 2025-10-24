using OpenQA.Selenium;

namespace Selenium.Framework.Core
{
    public interface ILogger
    {
        void Log(string message);
        void TakeSnapshot(IWebDriver driver, string key);
    }
}
