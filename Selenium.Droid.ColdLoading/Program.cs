
using Selenium.Framework.Core;
using Selenium.Framework.Core.Driver;
using System;

namespace Selenium.Droid.ColdLoading
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args[0]=="MoneyHub") {
                WebPage webPage = new WebPage(Browser.Type.Chrome, @"http://192.168.1.230:6006");
                try
                {
                    //goto reboot page
                    webPage.Clicks("navbar-menu-items");
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    Browser.Close(webPage);
                }
            }
        
        }
    }
}
