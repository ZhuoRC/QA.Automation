using OpenQA.Selenium;
using Selenium.Framework.Core.Models;
using System.IO;

namespace Selenium.Framework.Core
{

    public enum BrowserType { Chrome, Firefox, IE, Opera };

    public class Browser
    {
        public BrowserType Type { get; set; }



        public string SeleniumWebDriverPath
        {
            get
            {
                dynamic objType = (new Site()).GetType();
                return Path.GetDirectoryName(objType.Assembly.Location) + "\\Driver";
            }
            set { SeleniumWebDriverPath = value; }
        }

        public IBrowserExtension BrowserExtension { get; set; }

        public Browser(string type, IBrowserExtension ext = null)
        {

            if (type.Equals("chrome"))
            {
                this.Type = BrowserType.Chrome;
                if (ext != null)
                {
                    this.BrowserExtension = (ChromeExtension)ext;
                }
            }
            if (type.Equals("firefox"))
            {
                this.Type = BrowserType.Firefox;
            }
            if (type.Equals("ie"))
            {
                this.Type = BrowserType.IE;
            }
            if (type.Equals("opera"))
            {
                this.Type = BrowserType.Opera;
            }
        }

        public IWebDriver Open()
        {
            return SeleniumWebDriverService.Open(this);
        }
    }


    public interface IBrowserExtension
    {

    }

    public class ChromeExtension : IBrowserExtension
    {
        public string ExtensionId { get; set; }
        public string ExtensionWebStoreKey { get; set; }
        public string ExtensionLocalPath { get; set; }

        public ChromeExtension(string id, string webStoreKey, string localPath)
        {
            this.ExtensionId = id;
            this.ExtensionWebStoreKey = webStoreKey;
            this.ExtensionLocalPath = localPath;
        }
    }
}
