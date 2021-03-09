using OpenQA.Selenium;

namespace Selenium.Framework.Core.Validation
{
    public interface IAssertion
    {
        void ExistElement(IWebElement webElement, string failMessage = "");
        void ExistText(string text, string failMessage = "");
        void NotExistText(string text, string failMessage = "");
        void MatchText(IWebElement webElement, string text, string failMessage = "");
        void MatchValue(IWebElement webElement, string text, string failMessage = "");

        void SelectedOption(IWebElement webElement, string text, string failMessage = "");
    }
}
