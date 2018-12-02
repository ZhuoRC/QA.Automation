using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.Framework.Core.Driver
{
    public static class Browser
    {
        public enum Type {Chrome, Firefox };

        public static object Open(Enum type)
        {
            if (type.Equals(Type.Chrome))
            {
                ChromeDriver driver = new ChromeDriver(@".\Driver\");
                return driver;
            }

            return null;

        }
    }
}
