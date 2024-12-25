using AutoMapper;
using FeatureResult.src.Shared.Abstractions;

namespace FeatureResult.src.Features.User
{
    public class UserController(IUserRepository repository, IMapper mapper) : EntityControllerBase<UserEntity>(repository, mapper)
    {
        // Add your specific methods here.
    }
}