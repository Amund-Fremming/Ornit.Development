using Ornit.Backend.src.Shared.Abstractions;

namespace Ornit.Backend.src.Features.User
{
    public class UserController(ILogger<UserController> logger, IUserRepository repository) : EntityControllerBase<UserEntity>(logger, repository)
    {
        // Add your specific methods here.
    }
}