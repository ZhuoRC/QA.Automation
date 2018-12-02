using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Framework.Core;
using Selenium.Framework.Core.Driver;
using System;

namespace Selenium.Droid.RebootRouter
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            WebPage router = new WebPage(Browser.Type.Chrome,@"http://192.168.1.1");

            try
            {

                //login
                router.EditInputText("userName", configuration.GetSection("username").Value);
                router.EditInputText("pcPassword", configuration.GetSection("password").Value);
                router.Click("loginBtn");

                //goto reboot page
                router.Click("menu_xtgl");
                router.Click("menu_restart");

                //reboot router
                router.Click("button_reboot");
                router.ClickAlertMessage("OK");

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Browser.Close(router);
            }

        }
    }
}
