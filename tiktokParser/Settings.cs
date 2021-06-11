using System;

namespace webParser
{
    public class Settings
    {
        public String DefaultParser = Available.Parser()[new Random().Next(0, Available.Parser().Count)];
        public String Url;
        public bool Headless = true;
        public bool ShortUrl = false;
        public bool DisableGpu = true;
        public String DefaultBrowser = "firefox";
        public String OutPutFile = "url";

        public String BrowserBinaryPath;
    }
}