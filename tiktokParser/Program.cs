using System;
using System.IO;

namespace tiktokParser
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Settings settings = new ArgumentParser(args).Parse();

            Parser url = new Parser(settings);
            
            try
            {
                File.WriteAllText(settings.OutPutFile, url.Parse());
                Environment.Exit(0);
            }
            catch (Exception)
            {
                Environment.Exit(1);
            }
        }
    }
}