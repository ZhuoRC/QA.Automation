using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.Framework.Core
{
    public static class ConsoleLogger
    {
        public static void Log(string message)
        {
            Console.WriteLine($"------ {DateTime.Now} >>>>>");
            Console.WriteLine(message);
        }

    }
}
