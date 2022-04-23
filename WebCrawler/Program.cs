using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            GetWebContentAsync();
            Console.ReadKey();
        }

        public static async Task GetWebContentAsync()
        {
            var url = "https://www.automobile.tn/fr/neuf/bmw";
            var httpClient = new HttpClient();
            var htmlContent = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlContent);
            var divs = htmlDocument.DocumentNode.Descendants("div")
                      .Where(c => c.GetAttributeValue("class", " ").Equals("versions-item")).ToList();

            foreach (var div in divs)
            {
                var heading = div.Descendants("h2").FirstOrDefault().InnerText;
                var price = div.Descendants("div")
                    .Where(c => c.GetAttributeValue("class", " ").Equals("price-ht")).FirstOrDefault().InnerText;
            }
        }
    }
}
