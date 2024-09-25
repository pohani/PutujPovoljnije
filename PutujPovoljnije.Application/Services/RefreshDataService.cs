using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PutujPovoljnije.Application.Interfaces;
using PutujPovoljnije.Domain.Models;
using PutujPovoljnije.Domain.Settings;

namespace PutujPovoljnije.Domain.Interfaces
{
    public class RefreshDataService : IRefreshDataService
    {
        private readonly IDataScrapeService _dataScrapeService;
        private readonly IAirportRepository _airportRepository;
        private readonly ILogger<RefreshDataService> _logger;
        private readonly WebScrapingSettings _webScrapingSettings;

        // Define the character list as a static readonly field, as it doesn't change
        private static readonly List<char> _letters = new List<char>()
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        public RefreshDataService(
            IDataScrapeService dataScrapeService,
            IAirportRepository airportRepository,
            IConfiguration configuration,
            ILogger<RefreshDataService> logger,
            IOptions<WebScrapingSettings> webScrapingSettings)
        {
            _dataScrapeService = dataScrapeService;
            _airportRepository = airportRepository;
            _logger = logger;
            _webScrapingSettings = webScrapingSettings.Value;
        }

        public async Task RefreshAirports()
        {
            try
            {
                _logger.LogInformation("Starting the refresh process for airports.");
                var airports = new List<Airport>();

                foreach (var letter in _letters)
                {
                    try
                    {
                        _logger.LogInformation("Scraping airports for letter: {Letter}", letter);
                        var scrapedAirports = await _dataScrapeService.ScrapeAirports($"{_webScrapingSettings.IATATableWebPage}{letter}");
                        airports.AddRange(scrapedAirports);
                        _logger.LogInformation("Successfully scraped {Count} airports for letter: {Letter}", scrapedAirports.Count, letter);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error occurred while scraping airports for letter: {Letter}", letter);
                        continue;
                    }
                }

                _logger.LogInformation("Fetched a total of {Count} airports after scraping.", airports.Count);

                var airportsDb = await _airportRepository.GetAirports();

                var airportsToDelete = airportsDb.Where(dbAirport => !airports.Any(a => a.IATA == dbAirport.IATA)).ToList();
                if (airportsToDelete.Any())
                {
                    _logger.LogInformation("Removing {Count} airports from the database.", airportsToDelete.Count);
                    await _airportRepository.RemoveAirports(airportsToDelete);
                }

                var airportsToAdd = airports.Where(a => !airportsDb.Any(dbAirport => dbAirport.IATA == a.IATA)).ToList();
                if (airportsToAdd.Any())
                {
                    _logger.LogInformation("Adding {Count} new airports to the database.", airportsToAdd.Count);
                    await _airportRepository.AddAirports(airportsToAdd);
                }

                _logger.LogInformation("Finished the refresh process for airports successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the refresh process.");
                throw;
            }
        }
    }
}
