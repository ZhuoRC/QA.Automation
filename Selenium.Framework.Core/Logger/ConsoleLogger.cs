using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.Framework.Core.Logger
{
    public static class ConsoleLogger
    {
        public static void Log(string message)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine(DateTime.Now);
            Console.WriteLine(message);
        }
    }
}
