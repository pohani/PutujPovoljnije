using PutujPovoljnije.Application.DTOs;

namespace PutujPovoljnije.Application.Interfaces
{
    public interface IAirportsService
    {
        Task<List<AirportDto>> GetAllAirports();
        Task<List<AirportDto>> GetAirports(int offset, int limit);
        Task<List<AirportDto>> GetAirportsFromString(string searchString);
    }
}
