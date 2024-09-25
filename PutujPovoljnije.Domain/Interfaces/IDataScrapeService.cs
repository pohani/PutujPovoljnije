using PutujPovoljnije.Domain.Models;

namespace PutujPovoljnije.Domain.Interfaces
{
    public interface IDataScrapeService
    {
        Task<List<Airport>> ScrapeAirports(string url);
    }
}
