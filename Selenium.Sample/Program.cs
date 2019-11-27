using Selenium.Framework.Core;
using Selenium.Framework.Core.Driver;
using System;

namespace Selenium.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ColdLoading");
            ColdLoading();
        }

        static void ColdLoading()
        {
            WebPage webPage = new WebPage(Browser.Type.Chrome, @"http://www.google.com");
            try
            {
                webPage.ClickByXPath("/html/body/div[1]/div[4]/form/div[2]/div[1]/div[3]/center/input[2]");
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
