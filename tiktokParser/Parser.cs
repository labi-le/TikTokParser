using System;
using System.IO;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace webParser
{
    public class Parser
    {
        private String url;
        private IWebDriver browser;
        private String parser;
        private bool isShort;


        public Parser(IWebDriver browser, String url, String parser, bool isShort)
        {
            this.url = url;
            this.browser = browser;
            this.parser = parser;
            this.isShort = isShort;
        }

        public string Parse()
        {
            return isShort ? ShortUrl(SelectParser()) : SelectParser();
        }

        private string ShortUrl(string longUrl)
        {
            WebRequest request = WebRequest.Create("https://clck.ru/--?url=" + longUrl);
            StreamReader streamReader = new StreamReader(request.GetResponse().GetResponseStream());
            return streamReader.ReadToEnd();
        }

        private string SelectParser()
        {
            switch (parser)
            {
                case "snaptik":
                    return SnapTik();
                case "ssstik":
                    return SssTik();
                case "ttdownloader":
                    return TtDownloader();
                case "musicaldown":
                    return MusicalDown();
                case "savefrom":
                    return SaveFrom();
                case "tiktokfull":
                    return TikTokFull();
            }

            return null;
        }

        private string TikTokFull()
        {
            browser.Navigate().GoToUrl("https://tiktokfull.com/");

            IWebElement inputElement = new WebDriverWait(browser, TimeSpan.FromSeconds(5))
                .Until(ExpectedConditions.ElementIsVisible(
                    By.CssSelector("#input-url")));

            inputElement.SendKeys(url);

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            inputElement.SendKeys(Keys.Enter);

            IWebElement button = new WebDriverWait(browser, TimeSpan.FromSeconds(15))
                .Until(ExpectedConditions.ElementExists(
                    By.CssSelector("a.download-link:nth-child(2)")));

            return button.GetAttribute("href");
        }

        private string SaveFrom()
        {
            browser.Navigate().GoToUrl("https://en.savefrom.net/download-from-tiktok");

            IWebElement inputElement = new WebDriverWait(browser, TimeSpan.FromSeconds(7))
                .Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#sf_url")));
            inputElement.SendKeys(url);

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            inputElement.SendKeys(Keys.Enter);


            IWebElement button = new WebDriverWait(browser, TimeSpan.FromSeconds(7))
                .Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".download-icon")));

            return button.GetAttribute("href");
        }

        private string MusicalDown()
        {
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            browser.Navigate().GoToUrl("https://musicaldown.com");

            IWebElement inputElement = browser.FindElement(By.CssSelector("#link_url"));
            inputElement.SendKeys(url);

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            inputElement.SendKeys(Keys.Enter);

            IWebElement downloadButton = browser.FindElement(By.CssSelector("a.btn:nth-child(8)"));

            return downloadButton.GetAttribute("href");
        }

        private string TtDownloader()
        {
            browser.Navigate().GoToUrl("https://ttdownloader.com" + "?url=" + url);

            IWebElement button = new WebDriverWait(browser, TimeSpan.FromSeconds(7))
                .Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("//html/body/section/div/div/div/div[1]/form/div[2]/div/div[1]/div[2]/a")));

            return button.GetAttribute("href");
        }

        private string SssTik()
        {
            browser.Navigate().GoToUrl("https://ssstik.io/ru");

            IWebElement inputElement = new WebDriverWait(browser, TimeSpan.FromSeconds(7))
                .Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("//*[@id=\"main_page_text\"]")));

            inputElement.SendKeys(url);

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            inputElement.SendKeys(Keys.Enter);


            IWebElement button = new WebDriverWait(browser, TimeSpan.FromSeconds(7))
                .Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("/html/body/main/section[1]/div/div/div[3]/div/div/a[2]")));

            return button.GetAttribute("href");
        }

        private string SnapTik()
        {
            browser.Navigate().GoToUrl("https://snaptik.app/ru");

            IWebElement inputElement = new WebDriverWait(browser, TimeSpan.FromSeconds(7))
                .Until(ExpectedConditions.ElementIsVisible((By.XPath("//*[@id=\"url\"]"))));

            inputElement.SendKeys(url);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            inputElement.SendKeys(Keys.Enter);

            IWebElement downloadButton = new WebDriverWait(browser, TimeSpan.FromSeconds(7))
                .Until(ExpectedConditions.ElementExists(
                    (By.XPath("/html/body/div[3]/section/div/div[1]/div/article/div[2]/div/a[4]"))));

            String directLink = downloadButton.GetAttribute("href");
            Console.WriteLine(directLink);
            return directLink;
        }
    }
}