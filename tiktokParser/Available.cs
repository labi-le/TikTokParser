using System.Collections.Generic;

namespace tiktokParser
{
    public class Available
    {
        public static readonly string Notice = "Notice:" +
                                               "\n@Copyright labile 2021" +
                                               "\nhttps://github.com/labi-le/" +
                                               "\n\n" +
                                               " Use " + string.Join(", ", ParserArgs()) +
                                               " to choose parse, available parsers:\n  " +
                                               string.Join(", ", Parser()) +
                                               "\n Use " + string.Join(", ", HeadlessArgs()) +
                                               " to see the parsing process" +
                                               "\n Use " + string.Join(", ", UrlArgs()) + " to parse url" +
                                               "\n Use " + string.Join(", ", ShortUrlArgs()) + " to shorten the link" +
                                               "\n Use " + string.Join(", ", DisableGpuArgs()) + " to don't use gpu" +
                                               "\n Use " + string.Join(", ", OutPutFile()) +
                                               " to set file in which the link will be saved" +
                                               "\n Use " + string.Join(", ", SetBrowserBinaryPathArgs()) +
                                               " set browser binary path" +
                                               "\n Use " + string.Join(", ", BrowserArgs()) +
                                               " to choose browser, available browsers: " +
                                               string.Join(", ", Browser()) +
                                               "\n\n" +
                                               " DEFAULT: parser random, headless true, shorting url false, browser firefox, gpu disabled";

        public static List<string> OutPutFile()
        {
            return new List<string> {"--output-file", "-o"};
        }

        public static List<string> Parser()
        {
            return new List<string>
                {"api-wrapper", "api-snaptik", "snaptik", "ssstik", "ttdownloader", "musicaldown", "savefrom", "tiktokfull"};
        }

        public static List<string> Browser()
        {
            return new List<string> {"firefox", "chromium"};
        }


        public static List<string> ParserArgs()
        {
            return new List<string> {"--parser", "-p"};
        }

        public static List<string> SetBrowserBinaryPathArgs()
        {
            return new List<string> {"--set-browser-binary-path", "-set-binary"};
        }

        public static List<string> BrowserArgs()
        {
            return new List<string> {"--browser", "-b"};
        }

        public static List<string> UrlArgs()
        {
            return new List<string> {"--url", "-u"};
        }

        public static List<string> HeadlessArgs()
        {
            return new List<string> {"--headless", "-h"};
        }

        public static List<string> ShortUrlArgs()
        {
            return new List<string> {"--short-url", "-s"};
        }

        public static List<string> DisableGpuArgs()
        {
            return new List<string> {"--disable-gpu", "-d"};
        }
    }
}