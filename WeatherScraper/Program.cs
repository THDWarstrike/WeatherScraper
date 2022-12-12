using System;
using System.Linq;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using CsvHelper;

namespace WeatherScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            // Download the HTML from weather.com
            var html = new WebClient().DownloadString("https://weather.com");

            // Load the HTML into an HtmlDocument
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            // Get all the forecast elements
            var forecastElements = htmlDoc.DocumentNode.SelectNodes("//a[contains(@class, 'clickable')]");

            // Create a CSV file
            using (var csvfile = File.CreateText("10-day-forecast.csv"))
            {
                var csvWriter = new CsvWriter(csvfile);

                // For each forecast element, write the date and forecast to the CSV file
                foreach (var element in forecastElements)
                {
                    var date = element.SelectSingleNode(".//span[contains(@class, 'date-time')]").InnerText.Trim();
                    var forecast = element.SelectSingleNode(".//td[contains(@class, 'description')]").InnerText.Trim();
                    csvWriter.WriteRecord(new { date, forecast });
                }
            }
        }
    }
}

