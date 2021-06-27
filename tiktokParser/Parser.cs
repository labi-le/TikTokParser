using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace tiktokParser
{
    public class Parser
    {
        private readonly string _url;
        private readonly string _parser;
        private readonly bool _isShort;
        private readonly Settings _settings;


        public Parser(Settings settings)
        {
            _url = settings.Url;
            _parser = settings.DefaultParser;
            _isShort = settings.ShortUrl;
            _settings = settings;
        }

        public string Parse()
        {
            return _isShort ? ShortUrl(SelectParser()) : SelectParser();
        }


        private string ShortUrl(string longUrl)
        {
            WebRequest request = WebRequest.Create("https://clck.ru/--?url=" + longUrl);
            StreamReader streamReader = new StreamReader(request.GetResponse().GetResponseStream()!);
            return streamReader.ReadToEnd();
        }

        private string SelectParser()
        {
            return _parser switch
            {
                "snaptik" => SnapTik(new Browser(_settings).Get()),
                "ssstik" => SssTik(new Browser(_settings).Get()),
                "ttdownloader" => TtDownloader(new Browser(_settings).Get()),
                "musicaldown" => MusicalDown(new Browser(_settings).Get()),
                "savefrom" => SaveFrom(new Browser(_settings).Get()),
                "tiktokfull" => TikTokFull(new Browser(_settings).Get()),
                "api-wrapper" => ApiWrapper(),
                "api-snaptik" => ApiSnaptik(new Browser(_settings).Get()),
                _ => null
            };
        }

        private string ApiWrapper()
        {
            HttpClient client = new HttpClient();

            Uri url = new Uri("https://freevideosdowloader.tk/services/downloader_api.php");

            FormUrlEncodedContent content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    {"url", _url}
                }
            );

            Task<HttpResponseMessage> task = client.PostAsync(url, content);
            dynamic deserializeObject = JObject.Parse(task.Result.Content.ReadAsStringAsync().Result);

            string directLink = deserializeObject.VideoResult[0].VideoUrl;

            Console.WriteLine(directLink);
            return directLink == String.Empty ? throw new Exception("Empty string") : directLink;
        }

        private string ApiSnaptik(IWebDriver browser)
        {
            bool IsAlertShown(IWebDriver driver)
            {
                try
                {
                    driver.SwitchTo().Alert().Accept();
                }
                catch (NoAlertPresentException)
                {
                    return false;
                }

                return true;
            }

            browser.Navigate().GoToUrl("https://snaptik.app/abc.php?url=" + _url);
            try
            {
                WebDriverWait wait = new WebDriverWait(browser, TimeSpan.FromSeconds(20));
                wait.Until(IsAlertShown);

                var regex = new Regex(@"\b(?:https?://|www\.)\S+\b",
                    RegexOptions.Compiled | RegexOptions.IgnoreCase);
                
                string url = regex.Matches(browser.PageSource)[2].ToString();
                browser.Close();

                return url;
            }
            catch (Exception e)
            {
                browser.Close();
                throw new Exception(e.Message);
            }
        }

        private string TikTokFull(IWebDriver browser)
        {
            browser.Navigate().GoToUrl("https://tiktokfull.com/");

            try
            {
                IWebElement inputElement = new WebDriverWait(browser, TimeSpan.FromSeconds(5))
                    .Until(ExpectedConditions.ElementIsVisible(
                        By.CssSelector("#input-url")));

                inputElement.SendKeys(_url);

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                inputElement.SendKeys(Keys.Enter);

                IWebElement button = new WebDriverWait(browser, TimeSpan.FromSeconds(15))
                    .Until(ExpectedConditions.ElementExists(
                        By.CssSelector("a.download-link:nth-child(2)")));

                browser.Close();

                return button.GetAttribute("href");
            }
            catch (Exception e)
            {
                browser.Close();
                throw new Exception(e.Message);
            }
        }

        private string SaveFrom(IWebDriver browser)
        {
            browser.Navigate().GoToUrl("https://en.savefrom.net/download-from-tiktok");
            try
            {
                IWebElement inputElement = new WebDriverWait(browser, TimeSpan.FromSeconds(7))
                    .Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#sf_url")));
                inputElement.SendKeys(_url);

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                inputElement.SendKeys(Keys.Enter);


                IWebElement button = new WebDriverWait(browser, TimeSpan.FromSeconds(7))
                    .Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".download-icon")));

                browser.Close();

                return button.GetAttribute("href");
            }
            catch (Exception e)
            {
                browser.Close();
                throw new Exception(e.Message);
            }
        }

        private string MusicalDown(IWebDriver browser)
        {
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            browser.Navigate().GoToUrl("https://musicaldown.com");

            try
            {
                IWebElement inputElement = browser.FindElement(By.CssSelector("#link_url"));
                inputElement.SendKeys(_url);

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                inputElement.SendKeys(Keys.Enter);

                IWebElement downloadButton = browser.FindElement(By.CssSelector("a.btn:nth-child(8)"));

                browser.Close();

                return downloadButton.GetAttribute("href");
            }
            catch (Exception e)
            {
                browser.Close();
                throw new Exception(e.Message);
            }
        }

        private string TtDownloader(IWebDriver browser)
        {
            browser.Navigate().GoToUrl("https://ttdownloader.com" + "?url=" + _url);

            try
            {
                IWebElement button = new WebDriverWait(browser, TimeSpan.FromSeconds(7))
                    .Until(ExpectedConditions.ElementIsVisible(
                        By.XPath("//html/body/section/div/div/div/div[1]/form/div[2]/div/div[1]/div[2]/a")));

                browser.Close();

                return button.GetAttribute("href");
            }
            catch (Exception e)
            {
                browser.Close();
                throw new Exception(e.Message);
            }
        }

        private string SssTik(IWebDriver browser)
        {
            browser.Navigate().GoToUrl("https://ssstik.io/ru");

            try
            {
                IWebElement inputElement = new WebDriverWait(browser, TimeSpan.FromSeconds(7))
                    .Until(ExpectedConditions.ElementIsVisible(
                        By.XPath("//*[@id=\"main_page_text\"]")));

                inputElement.SendKeys(_url);

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

                inputElement.SendKeys(Keys.Enter);


                IWebElement button = new WebDriverWait(browser, TimeSpan.FromSeconds(7))
                    .Until(ExpectedConditions.ElementToBeClickable(
                        By.XPath("/html/body/main/section[1]/div/div/div[3]/div/div/a[2]")));

                browser.Close();

                Console.WriteLine(button.GetAttribute("href"));
                return button.GetAttribute("href");
            }
            catch (Exception e)
            {
                browser.Close();
                throw new Exception(e.Message);
            }
        }

        private string SnapTik(IWebDriver browser)
        {
            browser.Navigate().GoToUrl("https://snaptik.app/ru");

            try
            {
                IWebElement inputElement = new WebDriverWait(browser, TimeSpan.FromSeconds(7))
                    .Until(ExpectedConditions.ElementIsVisible((By.XPath("//*[@id=\"url\"]"))));

                inputElement.SendKeys(_url);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

                inputElement.SendKeys(Keys.Enter);

                IWebElement downloadButton = new WebDriverWait(browser, TimeSpan.FromSeconds(7))
                    .Until(ExpectedConditions.ElementExists(
                        (By.XPath("/html/body/main/section[2]/div/div/article/div[2]/div/a[3]"))));

                string directLink = downloadButton.GetAttribute("href");
                Console.WriteLine(directLink);

                browser.Close();

                return directLink;
            }
            catch (Exception e)
            {
                browser.Close();
                throw new Exception(e.Message);
            }
        }
    }
}