using System;
using System.Collections.Generic;

namespace webParser
{
    public class Available
    {
        public static String Notice = "Notice:" +
                                      "\n@Copyright labile 2021" +
                                      "\nhttps://github.com/labi-le/" +
                                      "\n\n" +
                                      " Use " + String.Join(", ", ParserArgs()) +
                                      " to choose parse, available parsers:\n  " + String.Join(", ", Parser()) +
                                      "\n Use word random to use random parser" +
                                      "\n Use " + String.Join(", ", HeadlessArgs()) + " to see the parsing process" +
                                      "\n Use " + String.Join(", ", UrlArgs()) + " to parse url" +
                                      "\n Use " + String.Join(", ", ShortUrlArgs()) + " to shorten the link" +
                                      "\n Use " + String.Join(", ", DisableGpuArgs()) + " to don't use gpu" +
                                      "\n Use " + String.Join(", ", OutPutFile()) + " to set file in which the link will be saved" +
                                      "\n Use " + String.Join(", ", SetBrowserBinaryPathArgs()) + " set browser binary path" +
                                      "\n Use " + String.Join(", ", BrowserArgs()) +
                                      " to choose browser, available browsers: " +
                                      String.Join(", ", Browser()) +
                                      "\n\n" +
                                      " DEFAULT: parser random, headless true, shorting url false, browser firefox, gpu disabled";

        public static List<string> OutPutFile()
        {
            return new List<string> {"--output-file", "-o"};
        }

        public static List<String> Parser()
        {
            return new List<String>
                {"snaptik", "ssstik", "ttdownloader", "musicaldown", "savefrom", "tiktokfull"};
        }

        public static List<String> Browser()
        {
            return new List<String> {"firefox", "chromium"};
        }


        public static List<String> ParserArgs()
        {
            return new List<String> {"--parser", "-p"};
        }

        public static List<String> SetBrowserBinaryPathArgs()
        {
            return new List<String> {"--set-browser-binary-path", "-set-binary"};
        }

        public static List<String> BrowserArgs()
        {
            return new List<String> {"--browser", "-b"};
        }

        public static List<String> UrlArgs()
        {
            return new List<String> {"--url", "-u"};
        }

        public static List<String> HeadlessArgs()
        {
            return new List<String> {"--headless", "-h"};
        }

        public static List<String> ShortUrlArgs()
        {
            return new List<String> {"--short-url", "-s"};
        }

        public static List<String> DisableGpuArgs()
        {
            return new List<String> {"--disable-gpu", "-d"};
        }
    }
}