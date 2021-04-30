using System;
using System.IO;

namespace KadishmanXML
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get sitemap file path from arguments.
            if (args.Length == 0)
            {
                Console.Error.WriteLine(
                    "path to sitemap.xml file must be provided as argument.");

                Environment.ExitCode = -1;
                return;
            }

            var siteMapFilePath = args[0];

            // Get path of the output file.
            String outputFilePath;

            if (args.Length == 2)
            {
                outputFilePath = args[1];
            }
            else
            {
                var sourceDir = Path.GetDirectoryName(siteMapFilePath);
                outputFilePath = Path.Join(sourceDir, "urls.csv");
            } 
                    
            // Extract the URLs from the sitemap into the output file.
            var extractor = new SiteMapUrlExtractor();

            try
            {
                extractor.Extract(siteMapFilePath, outputFilePath);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Environment.ExitCode = -1;
                return;
            }

            // Announce successful exection.
            Console.WriteLine($"Succesfuly extracted urls into file '{outputFilePath}'");
        }
    }
}
