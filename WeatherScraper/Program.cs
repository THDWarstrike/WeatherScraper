using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace WeatherScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set the URL of the page we want to scrape
            string url = "https://weather.com/weather/tenday/l/USNY0996:1:US";

            // Download the page as a string
            string page = new WebClient().DownloadString(url);

            // Use a regular expression to find the 10-day forecast in the page
            MatchCollection matches = Regex.Matches(page, "<td class=\"twc-sticky-col\">(.*?)</td>");

            // Create a new string builder to store the forecast
            StringBuilder forecast = new StringBuilder();

            // Loop through the matches and append each one to the forecast string
            foreach (Match match in matches)
            {
                forecast.AppendLine(match.Groups[1].Value);
            }

            // Write the forecast to a .csv file
            File.WriteAllText("forecast.csv", forecast.ToString());

            Console.WriteLine("Forecast saved to forecast.csv");
        }
    }
}
