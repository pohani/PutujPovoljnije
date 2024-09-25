using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using PutujPovoljnije.Domain.Interfaces;
using PutujPovoljnije.Domain.Models;

namespace PutujPovoljnije.Infrastructure.DataScraper
{
    public class DataScrapeService : IDataScrapeService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DataScrapeService> _logger;

        public DataScrapeService(HttpClient httpClient, ILogger<DataScrapeService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<Airport>> ScrapeAirports(string url)
        {
            _logger.LogInformation("Starting to scrape airports from URL: {Url}", url);

            string html;
            try
            {
                html = await _httpClient.GetStringAsync(url);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Failed to fetch HTML from URL: {Url}", url);
                throw;
            }

            var airports = ParseAirports(html);
            _logger.LogInformation("Successfully scraped {Count} airports.", airports.Count);
            return airports;
        }

        private List<Airport> ParseAirports(string html)
        {
            var airports = new List<Airport>();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var airportsRaw = htmlDoc.DocumentNode.SelectNodes("//table[contains(@class, 'wikitable')]//tr");

            if (airportsRaw == null)
            {
                _logger.LogWarning("No airport data found in the HTML document.");
                return airports;
            }

            foreach (var row in airportsRaw)
            {
                var cells = row.SelectNodes("td");
                if (cells != null && cells.Count > 3)
                {
                    var airport = new Airport
                    {
                        IATA = cells[0].InnerText.Trim(),
                        Name = cells[2].InnerText.Trim(),
                    };

                    SetAirportLocation(cells[3].InnerText.Trim(), airport);
                    airports.Add(airport);
                }
            }

            return airports;
        }

        private void SetAirportLocation(string locationText, Airport airport)
        {
            var locationParts = locationText.Split(',');
            switch (locationParts.Length)
            {
                case 3:
                    airport.City = locationParts[0].Trim();
                    airport.State = locationParts[1].Trim();
                    airport.Country = locationParts[2].Trim();
                    break;
                case 2:
                    airport.City = locationParts[0].Trim();
                    airport.State = null;
                    airport.Country = locationParts[1].Trim();
                    break;
                default:
                    airport.City = null;
                    airport.State = null;
                    airport.Country = locationText;
                    break;
            }
        }
    }
}
