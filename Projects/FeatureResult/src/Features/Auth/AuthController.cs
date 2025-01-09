using FeatureResult.src.Shared.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace FeatureResult.src.Features.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(ILogger<AuthController> logger, IConfiguration configuration) : ControllerBase
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            try
            {
                // TODO, register user entity in my db!!
                var auth0Domain = configuration["Auth0:Issuer"] ?? throw new ArgumentNullException("Auth0 issuer is null");
                var client_id = configuration["Auth0:ClientId"] ?? throw new ArgumentNullException("Auth0 client id is null");

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
                    return BadRequest($"Error registering user: {errorResponse}");
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                return Ok(responseBody);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Register");
                return StatusCode(500, "Something went wrong.");
            }
        }

        // Summary:
        //     This endpoint should not be used after testing.
        //     Call Auth0 servers directly from frontend instead.
        //
        // Remarks:
        //     Store values in a safe place.
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var auth0Domain = configuration["Auth0:Issuer"] ?? throw new ArgumentNullException("Auth0 issuer is null");
                var clientId = configuration["Auth0:ClientId"] ?? throw new ArgumentNullException("Auth0 issuer is null");
                var clientSecret = configuration["Auth0:ClientSecret"] ?? throw new ArgumentNullException("Auth0 issuer is null");
                var audience = configuration["Auth0:Audience"] ?? throw new ArgumentNullException("Auth0 issuer is null");

                var loginRequest = new
                {
                    grant_type = "password",
                    client_id = clientId,
                    client_secret = clientSecret,
                    username = email,
                    password,
                    audience,
                    scope = "offline_access",
                    connection = "Username-Password-Authentication"
                };

                var content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

                using var httpClient = new HttpClient();
                var response = await httpClient.PostAsync($"{auth0Domain}/oauth/token", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    return BadRequest($"Error logging in: {errorResponse}");
                }

                var rawServerResponse = await response.Content.ReadAsStringAsync();
                var serverResponse = JsonSerializer.Deserialize<ServerRegistrationResponse>(rawServerResponse);
                return Ok(serverResponse);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Login");
                return StatusCode(500, "Something went wrong.");
            }
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            try
            {
                var auth0Domain = configuration["Auth0:Issuer"] ?? throw new ArgumentNullException("Auth0 issuer is null");
                var clientId = configuration["Auth0:ClientId"] ?? throw new ArgumentNullException("Auth0 client id is null");
                var clientSecret = configuration["Auth0:ClientSecret"] ?? throw new ArgumentNullException("Auth0 client secret is null");

                var refreshTokenRequestBody = new
                {
                    grant_type = "refresh_token",
                    client_id = clientId,
                    client_secret = clientSecret,
                    refresh_token = refreshTokenRequest.RefreshToken,
                };

                var content = new StringContent(JsonSerializer.Serialize(refreshTokenRequestBody), Encoding.UTF8, "application/json");

                using var httpClient = new HttpClient();
                var response = await httpClient.PostAsync($"{auth0Domain}/oauth/token", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    return BadRequest($"Error refreshing token: {errorResponse}");
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                return Ok(responseBody);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Refresh token");
                return StatusCode(500, "Something went wrong.");
            }
        }

        [HttpPost("Authenticate")]
        [Authorize]
        public IActionResult Authenticate()
        {
            try
            {
                var userId = TokenExtractor.GetAuth0UserId(User);
                return Ok($"Valid token for user: {userId}");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Auth0AccessToken");
                throw;
            }
        }
    }
}