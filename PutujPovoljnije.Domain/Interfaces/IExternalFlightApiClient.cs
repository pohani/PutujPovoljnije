

using PutujPovoljnije.Domain.Models;

namespace PutujPovoljnije.Domain.Interfaces
{
    public interface IExternalFlightApiClient
    {
        Task<ApiResponse> GetFlightOffersAsync(string flightSearchString);
    }
}
