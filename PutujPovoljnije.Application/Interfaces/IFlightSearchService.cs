using PutujPovoljnije.Application.DTOs;

namespace PutujPovoljnije.Application.Interfaces
{
    public interface IFlightSearchService
    {
        Task<FlightSearchResultDto> SearchFlightsAsync(FlightSearchRequestDto request);
    }
}
