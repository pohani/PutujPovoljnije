using Microsoft.Extensions.Logging;
using PutujPovoljnije.Domain.Interfaces;
using System.Net.Http.Headers;

namespace PutujPovoljnije.Infrastructure.ExternalApiClients
{
    public class TokenHandler : DelegatingHandler
    {
        private readonly IAuthService _authService;
        private readonly ILogger<TokenHandler> _logger;

        public TokenHandler(IAuthService authService, ILogger<TokenHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var token = await _authService.GetTokenAsync();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _logger.LogInformation("Token successfully retrieved and added to the request headers.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve token for authorization.");
                throw;
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
