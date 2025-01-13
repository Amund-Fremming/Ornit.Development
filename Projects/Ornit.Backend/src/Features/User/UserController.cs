using AutoMapper;
using FeatureResult.src.Shared.Abstractions;

namespace FeatureResult.src.Features.User
{
    public class UserController(ILogger<UserController> logger, IUserRepository repository, IMapper mapper) : EntityControllerBase<UserEntity>(logger, repository, mapper)
    {
        // Add your specific methods here.
    }
}