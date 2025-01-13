using Ornit.Backend.src.Shared.ResultPattern;
using System.Text;
using System.Text.Json;

namespace Ornit.Backend.src.Features.Auth0
{
    public class AuthService(IConfiguration _configuration) : IAuthService
    {
        public async Task<Result<string>> RefreshAccessToken(RefreshTokenRequest request)
        {
            var auth0Domain = _configuration["Auth0:Issuer"] ?? throw new ArgumentNullException("Auth0 issuer is null");
            var clientId = _configuration["Auth0:ClientId"] ?? throw new ArgumentNullException("Auth0 client id is null");
            var clientSecret = _configuration["Auth0:ClientSecret"] ?? throw new ArgumentNullException("Auth0 client secret is null");

            var refreshTokenRequestBody = new
            {
                grant_type = "refresh_token",
                client_id = clientId,
                client_secret = clientSecret,
                refresh_token = request.RefreshToken,
            };

            var content = new StringContent(JsonSerializer.Serialize(refreshTokenRequestBody), Encoding.UTF8, "application/json");
            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsync($"{auth0Domain}/oauth/token", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return new Error($"Error refreshing token: {errorResponse}");
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        public async Task<Result<string>> Register(RegistrationRequest request)
        {
            var auth0Domain = _configuration["Auth0:Issuer"] ?? throw new ArgumentNullException("Auth0 issuer is null");
            var client_id = _configuration["Auth0:ClientId"] ?? throw new ArgumentNullException("Auth0 client id is null");

            var newUser = new
            {
                client_id,
                email = request.Email,
                password = request.Password,
                connection = "Username-Password-Authentication"
            };

            var content = new StringContent(JsonSerializer.Serialize(newUser), Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync($"{auth0Domain}/dbconnections/signup", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return new Error($"Error registering user: {errorResponse}");
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}