using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace FeatureResult.src.Features.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(ILogger<ApplicationUser> logger, IConfiguration configuration) : ControllerBase
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string email, string password)
        {
            try
            {
                // TODO, register user entity in my db!!
                var auth0Domain = configuration["Auth0:Issuer"] ?? throw new ArgumentNullException("Auth0 issuer is null");
                var client_id = configuration["Auth0:ClientId"] ?? throw new ArgumentNullException("Auth0 client id is null");

                var newUser = new
                {
                    client_id,
                    email,
                    password,
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
        //     This endpoint should not be used after testing. Call this endpoint from frontend.
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
                    scope = "openid profile email offline_access",
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

                var responseBody = await response.Content.ReadAsStringAsync();
                return Ok(responseBody);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Login");
                return StatusCode(500, "Something went wrong.");
            }
        }

        [HttpPost("Authenticate")]
        [Authorize]
        public IActionResult Authenticate()
        {
            try
            {
                return Ok("Valid token");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Auth0AccessToken");
                throw;
            }
        }
    }
}