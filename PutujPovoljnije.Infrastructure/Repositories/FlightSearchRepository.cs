using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PutujPovoljnije.Domain.Interfaces;
using PutujPovoljnije.Domain.Models;
using PutujPovoljnije.Infrastructure.Data;

namespace PutujPovoljnije.Infrastructure.Repositories
{
    public class FlightSearchRepository : IFlightSearchRepository
    {
        private readonly FlightSearchDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<FlightSearchRepository> _logger;

        public FlightSearchRepository(FlightSearchDbContext context, IMapper mapper, ILogger<FlightSearchRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddFlightSearchResults(FlightSearch flightSearch)
        {
            if (flightSearch == null)
            {
                _logger.LogWarning("Attempted to add null flight search results.");
                throw new ArgumentNullException(nameof(flightSearch), "Flight search results cannot be null.");
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

        public async Task<FlightSearch> SearchFlightsAsync(string flightSearchString)
        {
            nd  if (string.IsNullOrEmpty(flightSearchString))
            {
                _logger.LogWarning("Attempted to search flights with an empty search string.");
                throw new ArgumentException("Search string cannot be null or empty.", nameof(flightSearchString));
            }

            try
            {
                var flightSearch = await _context.FlightSearches
                    .Include(x => x.FlightOffers).ThenInclude(x => x.Price)
                    .Include(x => x.FlightOffers).ThenInclude(x => x.Itineraries).ThenInclude(x => x.Segments)
                    .FirstOrDefaultAsync(x => x.SearchString == flightSearchString);

                if (flightSearch != null)
                {
                    _logger.LogInformation("Flight search found for search string: {SearchString}", flightSearchString);
                }
                else
                {
                    _logger.LogInformation("No flight search found for search string: {SearchString}", flightSearchString);
                }

                return flightSearch;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching flights for search string: {SearchString}", flightSearchString);
                throw;
            }
        }
    }
}
