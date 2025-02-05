using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ornit.Backend.src.Shared.Abstractions;

namespace Ornit.Backend.src.Features.User
{
    public class UserController(ILogger<UserController> logger, IUserRepository repository) : EntityControllerBase<UserEntity>(logger, repository)
    {
        // remove
        [HttpGet("api/{param}")]
        [Authorize]
        public IActionResult Get(string param, string[] srs)
        {
            return Ok(new UserDto());
        }
    }
}