using System;
using System.IO;
using OpenQA.Selenium;

namespace webParser
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Settings settings = new ArgumentParser(args).Parse();

            IWebDriver driver = new Browser(settings).Get();
            Parser url = new Parser(driver, settings.Url, settings.DefaultParser, settings.ShortUrl);

            try
            {
                File.WriteAllText(settings.OutPutFile, url.Parse());
                driver.Close();
                Environment.Exit(0);
            }
            catch (Exception)
            {
                driver.Close();
                Environment.Exit(1);
            }
        }
    }
}