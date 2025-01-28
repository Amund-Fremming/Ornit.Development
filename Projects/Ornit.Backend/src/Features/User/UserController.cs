using Microsoft.AspNetCore.Mvc;
using Ornit.Backend.src.Features.Auth0;
using Ornit.Backend.src.Shared.Abstractions;

namespace Ornit.Backend.src.Features.User
{
    public class UserController(ILogger<UserController> logger, IUserRepository repository) : EntityControllerBase<UserEntity>(logger, repository)
    {
        [HttpGet]
        public IActionResult Get(HashSet<Auth0LoginResponse> loginreq)
        {
            return Ok();
        }
    }
}