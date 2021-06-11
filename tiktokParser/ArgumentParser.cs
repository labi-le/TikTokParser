using System;

namespace webParser
{
    public class ArgumentParser
    {
        private string[] args;
        private Settings Settings = new Settings();

        public ArgumentParser(String[] args)
        {
            this.args = args;
        }

        public Settings Parse()
        {
            if (args.Length < 2)
            {
                Notice();
            }

            for (int i = 0; i < args.Length; i++)
            {
                if (Available.ParserArgs().Contains(args[i]) && Available.Parser().Contains(args[i + 1]))
                {
                    Settings.DefaultParser = args[i + 1];
                }
                else if (Available.UrlArgs().Contains(args[i]))
                {
                    Settings.Url = args[i + 1];
                }
                else if (Available.OutPutFile().Contains(args[i]))
                {
                    Settings.OutPutFile = args[i + 1];
                }
                else if (Available.SetBrowserBinaryPathArgs().Contains(args[i]))
                {
                    Settings.OutPutFile = args[i + 1];
                }
                else if (Available.HeadlessArgs().Contains(args[i]))
                {
                    Settings.Headless = false;
                }
                else if (Available.BrowserArgs().Contains(args[i]) && Available.Browser().Contains(args[i + 1]))
                {
                    Settings.DefaultBrowser = args[i + 1];
                }
                else if (Available.ShortUrlArgs().Contains(args[i]))
                {
                    Settings.ShortUrl = true;
                }
                else if (Available.DisableGpuArgs().Contains(args[i]))
                {
                    Settings.DisableGpu = true;
                }
            }

            return Settings;
        }

        private void Notice()
        {
            Console.Write(Available.Notice);
            Environment.Exit(1);
        }
    }
}