using Microsoft.Extensions.Logging; // Added for logging
using Microsoft.Extensions.Options;
using PutujPovoljnije.Domain.Interfaces;
using PutujPovoljnije.Domain.Models;
using PutujPovoljnije.Domain.Settings;
using System.Text.Json;

namespace PutujPovoljnije.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthSettings _authSettings;
        private readonly ILogger<AuthService> _logger;

        public AuthService(HttpClient httpClient, IOptions<AuthSettings> authSettings, ILogger<AuthService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _authSettings = authSettings?.Value ?? throw new ArgumentNullException(nameof(authSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string> GetTokenAsync()
        {
            try
            {
                var tokenRequestBody = new Dictionary<string, string>
                {
                    { "grant_type", "client_credentials" },
                    { "client_id", _authSettings.ClientId },
                    { "client_secret", _authSettings.ClientSecret }
                };

                var requestContent = new FormUrlEncodedContent(tokenRequestBody);
                _logger.LogInformation("Sending token request to {TokenEndpoint}", _authSettings.TokenEndpoint);

                var response = await _httpClient.PostAsync(_authSettings.TokenEndpoint, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Failed to retrieve token: {ErrorResponse}", errorResponse);
                    throw new HttpRequestException($"Failed to retrieve token: {errorResponse}");
                }

                var tokenResponseStream = await response.Content.ReadAsStreamAsync();
                var tokenResponse = await JsonSerializer.DeserializeAsync<TokenResponse>(tokenResponseStream);

                if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
                {
                    _logger.LogError("Token response is invalid.");
                    throw new Exception("Token response is invalid.");
                }

                _logger.LogInformation("Successfully retrieved token.");
                return tokenResponse.AccessToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the token.");
                throw new Exception("An error occurred while fetching the token.", ex);
            }
        }
    }
}
