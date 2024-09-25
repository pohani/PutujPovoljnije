using AutoMapper;
using Microsoft.Extensions.Logging;
using PutujPovoljnije.Application.DTOs;
using PutujPovoljnije.Application.Interfaces;

namespace PutujPovoljnije.Domain.Interfaces
{
    public class AirportsService : IAirportsService
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AirportsService> _logger;

        public AirportsService(IAirportRepository airportRepository, IMapper mapper, ILogger<AirportsService> logger)
        {
            _airportRepository = airportRepository ?? throw new ArgumentNullException(nameof(airportRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<AirportDto>> GetAirports(int skip, int take)
        {
            try
            {
                _logger.LogInformation("Fetching airports with skip={Skip} and take={Take}", skip, take);
                var airports = await _airportRepository.GetAirports(skip, take);

                if (airports == null || airports.Count == 0)
                {
                    _logger.LogInformation("No airports found for the given pagination.");
                    return new List<AirportDto>();
                }

                _logger.LogInformation("Successfully fetched {Count} airports.", airports.Count);
                return _mapper.Map<List<AirportDto>>(airports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching airports.");
                throw new Exception("Error fetching airports.", ex);
            }
        }

        public async Task<List<AirportDto>> GetAirportsFromString(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                throw new ArgumentException("Search string cannot be null or empty.", nameof(searchString));
            }

            try
            {
                _logger.LogInformation("Searching for airports with search string: '{SearchString}'", searchString);
                var airports = await _airportRepository.SearchAirports(searchString);

                if (airports == null || airports.Count == 0)
                {
                    _logger.LogInformation("No airports found for the search string: '{SearchString}'", searchString);
                    return new List<AirportDto>();
                }

                _logger.LogInformation("Successfully found {Count} airports matching the search criteria.", airports.Count);
                return _mapper.Map<List<AirportDto>>(airports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching for airports with search string: '{SearchString}'", searchString);
                throw new Exception($"Error searching for airports with '{searchString}'.", ex);
            }
        }

        public async Task<List<AirportDto>> GetAllAirports()
        {
            try
            {
                _logger.LogInformation("Fetching all airports.");
                var airports = await _airportRepository.GetAirports();

                if (airports == null || airports.Count == 0)
                {
                    _logger.LogInformation("No airports found.");
                    return new List<AirportDto>();
                }

                _logger.LogInformation("Successfully fetched {Count} airports.", airports.Count);
                return _mapper.Map<List<AirportDto>>(airports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all airports.");
                throw new Exception("Error fetching all airports.", ex);
            }
        }
    }
}
