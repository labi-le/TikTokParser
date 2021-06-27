using System;

namespace tiktokParser
{
    public class ArgumentParser
    {
        private readonly string[] _args;
        private readonly Settings _settings = new Settings();

        public ArgumentParser(string[] args)
        {
            _args = args;
        }

        public Settings Parse()
        {
            if (_args.Length < 2)
            {
                Notice();
            }

            for (int i = 0; i < _args.Length; i++)
            {
                if (Available.ParserArgs().Contains(_args[i]) && Available.Parser().Contains(_args[i + 1]))
                {
                    _settings.DefaultParser = _args[i + 1];
                }

                if (Available.UrlArgs().Contains(_args[i]))
                {
                    string url = _args[i + 1];
                    if (CheckUrlValid(url))
                    {
                        _settings.Url = url;
                    }
                    else
                    {
                        Notice();
                    }
                }

                if (Available.OutPutFile().Contains(_args[i]))
                {
                    _settings.OutPutFile = _args[i + 1];
                }

                if (Available.SetBrowserBinaryPathArgs().Contains(_args[i]))
                {
                    _settings.BrowserBinaryPath = _args[i + 1];
                }

                if (Available.HeadlessArgs().Contains(_args[i]))
                {
                    _settings.Headless = false;
                }

                if (Available.BrowserArgs().Contains(_args[i]) && Available.Browser().Contains(_args[i + 1]))
                {
                    _settings.DefaultBrowser = _args[i + 1];
                }

                if (Available.ShortUrlArgs().Contains(_args[i]))
                {
                    _settings.ShortUrl = true;
                }

                if (Available.DisableGpuArgs().Contains(_args[i]))
                {
                    _settings.DisableGpu = true;
                }
            }

            return _settings;
        }

        private static void Notice()
        {
            Console.Write(Available.Notice);
            Environment.Exit(1);
        }


        private static bool CheckUrlValid(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) && uriResult.Scheme == Uri.UriSchemeHttps;
        }
    }
}