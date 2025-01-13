using System.Text.Json.Serialization;

namespace Ornit.Backend.src.Features.Auth0
{
    public record Auth0RegistrationResponse
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("email_verified")]
        public bool EmailVerified { get; set; }
    }
}