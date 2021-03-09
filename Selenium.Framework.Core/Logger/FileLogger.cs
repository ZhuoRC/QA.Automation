using OpenQA.Selenium;
using System;
using System.IO;

namespace Selenium.Framework.Core
{
    public class FileLogger : ILogger
    {
        public string loggerPath { get; set; }

        public string loggerScreenshotPath { get; set; }

        public FileLogger(string loggerName, string className)
        {
            string rootPath = @"C:\Logs\TestResults";

            string directoryPath = $@"{rootPath}\{DateTime.Now.ToString("yyyyMMdd_HHmmss")}_{className}_{loggerName}\";

            Directory.CreateDirectory(directoryPath);

            loggerScreenshotPath = $"{directoryPath}\\screenshot";
            Directory.CreateDirectory(loggerScreenshotPath);

            Directory.CreateDirectory($"{directoryPath}\\output");
            this.loggerPath = $"{directoryPath}\\execute.log";
            File.Create(loggerPath).Close();
        }

        public void Log(string logMessage)
        {

            ConsoleLogger.Log(logMessage);

            using (StreamWriter sw = File.AppendText(this.loggerPath))
            {
                try
                {
                    string logLine = String.Format(
                           "{0:G}: {1}", DateTime.Now, logMessage);
                    sw.WriteLine(logLine);
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    sw.Close();
                }
            }
        }

        public void TakeSnapshot(IWebDriver driver, string screenshotKey)
        {
            screenshotKey = string.Join("_", screenshotKey.Split(Path.GetInvalidFileNameChars()));

            Screenshot scrFile = ((ITakesScreenshot)driver).GetScreenshot();
            string file = $"{loggerScreenshotPath}\\{DateTime.Now.ToString("yyyyMMddHHmmss")}_{screenshotKey}.jpg";
            scrFile.SaveAsFile(file, ScreenshotImageFormat.Jpeg);

        }
    }
}
