using PutujPovoljnije.Domain.Models;

namespace PutujPovoljnije.Domain.Interfaces
{
    public interface IAirportRepository
    {
        Task<List<Airport>> GetAirports();
        Task AddAirports(List<Airport> airports);
        Task RemoveAirports(List<Airport> airports);
        Task<List<Airport>> SearchAirports(string searchString);
        Task<List<Airport>> GetAirports(int skip, int take);
    }
}
