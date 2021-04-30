using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Web;
using System.Text;

namespace KadishmanXML
{
    class SiteMapUrlExtractor
    {
        public void Extract(string siteMapFilePath, string outputFilePath)
        {
            // Check sitemap file exists.
            bool fileExists = !string.IsNullOrWhiteSpace(siteMapFilePath)
                && File.Exists(siteMapFilePath);

            if (!fileExists)
            {
                throw new ArgumentException(
                    nameof(siteMapFilePath),
                    "Please provide a valid path to a SiteMap XML file.");
            }

            // Load site-map xml file.
            XmlDocument xmlDocument = new XmlDocument();
            

            try
            {
                xmlDocument.Load(siteMapFilePath);
            }
            catch (XmlException e)
            {
                throw new Exception("File provided is not a valid XML.");
            }

            // Get all urls from sitemap (lazy).
            var urls = xmlDocument
                .SelectNodes("//*[local-name()='urlset']/*[local-name()='url']/*[local-name()='loc']")
                .Cast<XmlNode>()
                .Select(node => node.InnerText)
                .Select(HttpUtility.UrlDecode);

            // Write all urls to the file.
            File.WriteAllLines(outputFilePath, urls, Encoding.Unicode);

        }
    }
}