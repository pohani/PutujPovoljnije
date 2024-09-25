using PutujPovoljnije.Domain.Models;

namespace PutujPovoljnije.Domain.Interfaces
{
    public interface IFlightSearchRepository
    {
        Task<FlightSearch> SearchFlightsAsync(string flightSearchString);
        Task AddFlightSearchResults(FlightSearch flightSearch);
    }
}
