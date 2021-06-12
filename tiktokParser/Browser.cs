using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace tiktokParser
{
    public class Browser
    {
        private IWebDriver browser;

        public Browser(Settings settings)
        {
            if (settings.DefaultBrowser == "firefox")
            {
                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
                FirefoxOptions options = new FirefoxOptions();

                if (settings.BrowserBinaryPath != null)
                {
                    service.FirefoxBinaryPath = settings.BrowserBinaryPath;
                }

                if (settings.Headless)
                {
                    options.AddArgument("--headless");
                }

                if (settings.DisableGpu)
                {
                    options.AddArgument("--disable-gpu");
                }

                browser = new FirefoxDriver(service, options);
            }

            else if (settings.DefaultBrowser == "chromium")
            {
                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                ChromeOptions options = new ChromeOptions();

                if (settings.BrowserBinaryPath != null)
                {
                    options.BinaryLocation = settings.BrowserBinaryPath;
                }
                
                if (settings.Headless)
                {
                    options.AddArgument("--headless");
                }


                if (settings.DisableGpu)
                {
                    options.AddArgument("--disable-gpu");
                }


                browser = new ChromeDriver(service, options);
            }
        }

        public IWebDriver Get()
        {
            return browser;
        }
    }
}