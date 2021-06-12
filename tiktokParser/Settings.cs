using System;

namespace tiktokParser
{
    public class Settings
    {
        public string DefaultParser = Available.Parser()[new Random().Next(0, Available.Parser().Count)];
        public string Url;
        public bool Headless = true;
        public bool ShortUrl = false;
        public bool DisableGpu = true;
        public string DefaultBrowser = "firefox";
        public string OutPutFile = "url";

        public string BrowserBinaryPath;
    }
}