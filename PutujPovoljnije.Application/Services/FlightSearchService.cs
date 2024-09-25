using AutoMapper;
using Microsoft.Extensions.Logging;
using PutujPovoljnije.Application.DTOs;
using PutujPovoljnije.Application.Interfaces;
using PutujPovoljnije.Domain.Interfaces;
using PutujPovoljnije.Domain.Models;

namespace PutujPovoljnije.Application.Services
{
    public class FlightSearchService : IFlightSearchService
    {
        private readonly IFlightSearchRepository _repository;
        private readonly IExternalFlightApiClient _flightApiClient;
        private readonly ILogger<FlightSearchService> _logger;
        private readonly IMapper _mapper;

        public FlightSearchService(
            IFlightSearchRepository repository,
            IExternalFlightApiClient flightApiClient,
            ILogger<FlightSearchService> logger,
            IMapper mapper)
        {
            _repository = repository;
            _flightApiClient = flightApiClient;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<FlightSearchResultDto> SearchFlightsAsync(FlightSearchRequestDto request)
        {
            var flightSearch = _mapper.Map<FlightSearch>(request);
            flightSearch.SearchString = GenerateSearchString(flightSearch);

            try
            {
                _logger.LogInformation("Searching for saved flight search results with search string: {SearchString}", flightSearch.SearchString);

                var savedResults = await _repository.SearchFlightsAsync(flightSearch.SearchString);
                if (savedResults != null)
                {
                    _logger.LogInformation("Returning saved flight search results.");
                    return _mapper.Map<FlightSearchResultDto>(savedResults);
                }

                _logger.LogInformation("No saved results found. Querying external flight API.");
                var apiResponse = await _flightApiClient.GetFlightOffersAsync(flightSearch.SearchString);
                flightSearch.FlightOffers = apiResponse.Data;

                await _repository.AddFlightSearchResults(flightSearch);
                _logger.LogInformation("Successfully added flight search results to the repository.");

                return _mapper.Map<FlightSearchResultDto>(flightSearch);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for flights.");
                throw;
            }
        }

        private string GenerateSearchString(FlightSearch flightSearch)
        {
            return string.Join("&", new List<string>
            {
                !string.IsNullOrEmpty(flightSearch.DepartureAirport) ? $"originLocationCode={flightSearch.DepartureAirport}" : null,
                !string.IsNullOrEmpty(flightSearch.DestinationAirport) ? $"destinationLocationCode={flightSearch.DestinationAirport}" : null,
                !string.IsNullOrEmpty(flightSearch.DepartureDate) ? $"departureDate={flightSearch.DepartureDate}" : null,
                !string.IsNullOrEmpty(flightSearch.ReturnDate) ? $"returnDate={flightSearch.ReturnDate}" : null,
                flightSearch.Adults > 0 ? $"adults={flightSearch.Adults}" : null,
                flightSearch.Children >= 0 ? $"children={flightSearch.Children}" : null,
                !string.IsNullOrEmpty(flightSearch.Currency) ? $"currencyCode={flightSearch.Currency}" : null
            }.Where(param => param != null));
        }
    }
}
