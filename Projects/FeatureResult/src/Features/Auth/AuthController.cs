using FeatureResult.src.Features.Example;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NucleusResults.Core;

namespace FeatureResult.src.Features.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(ILogger<ApplicationUser> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService) : ControllerBase
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string email, string password)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    return BadRequest("Error creating user.");
                }
                var newUser = new ApplicationUser() { UserName = email, Email = email, };
                var result = await userManager.CreateAsync(newUser, password);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Aggregate("", (sum, next) => sum + " " + next.Description);
                    return BadRequest($"Error creating user:{errors}");
                }
                return tokenService.GenerateToken(newUser.Id, email)
                    .Resolve(suc => Ok(suc.Data),
                        err => BadRequest(err.Message));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Register");
                return StatusCode(500, "Something went wrong.");
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return BadRequest("Error logging in user.");
                }
                var isPasswordValid = await userManager.CheckPasswordAsync(user, password);
                if (!isPasswordValid)
                {
                    return BadRequest("Error logging in user.");
                }
                return tokenService.GenerateToken(user.Id, user.Email!)
                    .Resolve(suc => Ok(suc.Data),
                        err => BadRequest(err.Message));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Login");
                return StatusCode(500, "Something went wrong.");
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromServices] SignInManager<ExampleEntity> signInManager)
        {
            try
            {
                await signInManager.SignOutAsync();
                return Ok("User signed out.");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Logout");
                return StatusCode(500, "Something went wrong.");
            }
        }
    }
}