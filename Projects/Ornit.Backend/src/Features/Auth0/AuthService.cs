using Ornit.Backend.src.Shared.ResultPattern;
using System.Text.Json;

namespace Ornit.Backend.src.Features.Auth0
{
    public class AuthService(IConfiguration _configuration, IHttpClientFactory httpClientFactory, ILogger<AuthService> _logger) : IAuthService
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("auth0");

        // Summary:
        //     This method should not be used after testing.
        //     Call Auth0 servers directly from frontend instead.
        //
        // Remarks:
        //     Store values in a safe place.
        public async Task<Result<string>> RefreshAccessToken(string refreshToken)
        {
            try
            {
                var clientId = _configuration["Auth0:ClientId"] ?? throw new KeyNotFoundException("Auth0 client id is null");
                var clientSecret = _configuration["Auth0:ClientSecret"] ?? throw new KeyNotFoundException("Auth0 client secret is null");

                var values = new Dictionary<string, string>()
                {
                    { "grant_type", "refresh_token" },
                    { "client_id", clientId },
                    { "client_secret", clientSecret },
                    { "refresh_token", refreshToken },
                };

                var content = new FormUrlEncodedContent(values);
                var response = await _httpClient.PostAsync("/oauth/token", content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Error($"Error refreshing token.");
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "RefreshAccessToken");
                return new Error("Error refreshing access token.", e);
            }
        }

        public async Task<Result<Auth0RegisterResponse>> Register(AuthRequest request)
        {
            try
            {
                var clientId = _configuration["Auth0:ClientId"] ?? throw new KeyNotFoundException("Auth0 client id is null");

                var values = new Dictionary<string, string>()
                {
                    { "client_id", clientId },
                    { "email", request.Email },
                    { "password", request.Password },
                    { "connection", "Username-Password-Authentication" },
                };

                var content = new FormUrlEncodedContent(values);
                var response = await _httpClient.PostAsync("/dbconnections/signup", content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Error($"Error registering user.");
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                var auth0Response = JsonSerializer.Deserialize<Auth0RegisterResponse>(responseBody);
                if (auth0Response is null)
                {
                    return new Error("Error deserializing registration response from auth0");
                }

                return auth0Response;
            }
            catch (JsonException e)
            {
                _logger.LogError(e, "Register");
                return new Error("Error deserializing response.", e);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Register");
                return new Error("Error registering user.", e);
            }
        }

        public async Task<Result<Auth0LoginResponse>> Login(AuthRequest request)
        {
            var clientId = _configuration["Auth0:ClientId"] ?? throw new KeyNotFoundException("Auth0 client id is null");
            var clientSecret = _configuration["Auth0:ClientSecret"] ?? throw new KeyNotFoundException("Auth0 client secret is null");
            var audience = _configuration["Auth0:Audience"] ?? throw new KeyNotFoundException("Auth0 audience is null");

            var values = new Dictionary<string, string>()
            {
                { "grant_type", "password" },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "username", request.Email},
                { "password", request.Password},
                { "audience", audience},
                { "scope", "offline_access" },
                { "connection", "Username-Password-Authentication" },
            };

            var content = new FormUrlEncodedContent(values);
            var response = await _httpClient.PostAsync("/oauth/token", content);

            if (!response.IsSuccessStatusCode)
            {
                return new Error($"Error while logging in");
            }

            var serverResponse = await response.Content.ReadAsStringAsync();
            var auth0Response = JsonSerializer.Deserialize<Auth0LoginResponse>(serverResponse);
            if (auth0Response is null)
            {
                return new Error("Error logging in user.");
            }

            return auth0Response;
        }
    }
}