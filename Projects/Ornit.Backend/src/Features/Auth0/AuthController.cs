using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ornit.Backend.src.Shared.Common;
using Ornit.Backend.src.Shared.ResultPattern;
using System.Text;
using System.Text.Json;

namespace Ornit.Backend.src.Features.Auth0
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(ILogger<AuthController> logger, IConfiguration _configuration, IAuthService _authService) : ControllerBase
    {
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            try
            {
                var result = await _authService.RefreshAccessToken(refreshTokenRequest);
                return result.Resolve(suc => Ok(suc.Data),
                    err => BadRequest(err.Message));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Refresh token");
                return StatusCode(500, "Something went wrong.");
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            try
            {
                // TODO, register user entity in my db!!
                var result = await _authService.Register(request);
                return result.Resolve(suc => Ok(suc.Data),
                    err => BadRequest(err.Message));
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
                var auth0Domain = _configuration["Auth0:Issuer"] ?? throw new ArgumentNullException("Auth0 issuer is null");
                var clientId = _configuration["Auth0:ClientId"] ?? throw new ArgumentNullException("Auth0 issuer is null");
                var clientSecret = _configuration["Auth0:ClientSecret"] ?? throw new ArgumentNullException("Auth0 issuer is null");
                var audience = _configuration["Auth0:Audience"] ?? throw new ArgumentNullException("Auth0 issuer is null");

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

        // Summary:
        //     This endpoint is only used for testing if the
        //     token is parsed correclty.
        //
        // Remarks:
        //     Remove when development is finished.
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