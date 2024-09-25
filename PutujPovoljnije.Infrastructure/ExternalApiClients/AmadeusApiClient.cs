using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PutujPovoljnije.Domain.Interfaces;
using PutujPovoljnije.Domain.Models;

namespace PutujPovoljnije.Infrastructure.ExternalApiClients
{
    public class AmadeusApiClient : IExternalFlightApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AmadeusApiClient> _logger;

        public AmadeusApiClient(HttpClient httpClient, ILogger<AmadeusApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ApiResponse> GetFlightOffersAsync(string flightSearchString)
        {
            try
            {
                var response = await _httpClient.GetAsync($"v2/shopping/flight-offers?{flightSearchString}&max=10");
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

                _logger.LogInformation("Successfully retrieved flight offers.");
                return apiResponse;
            }
            catch (HttpRequestException httpRequestException)
            {
                _logger.LogError(httpRequestException, "Error fetching flight offers from the API.");
                throw;
            }
            catch (JsonException jsonException)
            {
                _logger.LogError(jsonException, "Error deserializing the flight offers response.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching flight offers.");
                throw;
            }
        }
    }
}
