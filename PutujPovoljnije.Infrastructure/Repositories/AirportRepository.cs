using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PutujPovoljnije.Domain.Interfaces;
using PutujPovoljnije.Domain.Models;
using PutujPovoljnije.Infrastructure.Data;

namespace PutujPovoljnije.Infrastructure.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly FlightSearchDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AirportRepository> _logger;

        public AirportRepository(FlightSearchDbContext context, IMapper mapper, ILogger<AirportRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddAirports(List<Airport> airports)
        {
            if (airports == null || airports.Count == 0)
            {
                _logger.LogWarning("Attempted to add null or empty list of airports.");
                throw new ArgumentNullException(nameof(airports), "Airports list cannot be null or empty.");
            }

            try
            {
                await _context.Airports.AddRangeAsync(airports);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully added {Count} airports.", airports.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding airports.");
                throw;
            }
        }

        public async Task AddFlightSearchResults(FlightSearch flightSearch)
        {
            if (flightSearch == null)
            {
                _logger.LogWarning("Attempted to add null flight search results.");
                throw new ArgumentNullException(nameof(flightSearch), "Flight search cannot be null.");
            }

            try
            {
                await _context.FlightSearches.AddAsync(flightSearch);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully added flight search results for search string: {SearchString}", flightSearch.SearchString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding flight search results for search string: {SearchString}", flightSearch.SearchString);
                throw;
            }
        }

        public async Task<List<Airport>> GetAirports()
        {
            try
            {
                return await _context.Airports.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving airports.");
                throw;
            }
        }

        public async Task<List<Airport>> GetAirports(int skip, int take)
        {
            try
            {
                return await _context.Airports
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving airports with skip: {Skip} and take: {Take}.", skip, take);
                throw;
            }
        }

        public async Task RemoveAirports(List<Airport> airports)
        {
            if (airports == null || airports.Count == 0)
            {
                _logger.LogWarning("Attempted to remove null or empty list of airports.");
                throw new ArgumentNullException(nameof(airports), "Airports list cannot be null or empty.");
            }

            try
            {
                _context.Airports.RemoveRange(airports);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully removed {Count} airports.", airports.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing airports.");
                throw;
            }
        }

        public async Task<List<Airport>> SearchAirports(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                _logger.LogWarning("Attempted to search airports with an empty search string.");
                throw new ArgumentException("Search string cannot be null or empty.", nameof(searchString));
            }

            try
            {
                searchString = searchString.ToLower();

                var airports = await _context.Airports.Where(
                    x => x.Country.ToLower().Contains(searchString) ||
                    x.State.ToLower().Contains(searchString) ||
                    x.City.ToLower().Contains(searchString) ||
                    x.Name.ToLower().Contains(searchString) ||
                    x.IATA.ToLower().Contains(searchString)
                ).ToListAsync();

                if (airports.Any())
                {
                    _logger.LogInformation("Found {Count} airports matching search string: {SearchString}", airports.Count, searchString);
                }
                else
                {
                    _logger.LogInformation("No airports found matching search string: {SearchString}", searchString);
                }

                return airports;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching airports for search string: {SearchString}", searchString);
                throw;
            }
        }
    }
}
