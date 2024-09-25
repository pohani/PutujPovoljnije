using System.Text.Json.Serialization;

namespace PutujPovoljnije.Domain.Models
{
    public class TokenResponse
    {
        public string Type { get; set; }
        public string Username { get; set; }
        public string ApplicationName { get; set; }
        public string ClientId { get; set; }
        public string TokenType { get; set; }
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string State { get; set; }
        public string Scope { get; set; }
    }

}
