using AutoMapper;
using Ornit.Backend.src.Shared.Abstractions;

namespace Ornit.Backend.src.Features.User
{
    public class UserController(ILogger<UserController> logger, IUserRepository repository, IMapper mapper) : EntityControllerBase<UserEntity>(logger, repository, mapper)
    {
        // Add your specific methods here.
    }
}