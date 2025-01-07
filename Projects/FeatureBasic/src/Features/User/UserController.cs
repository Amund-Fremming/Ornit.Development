using AutoMapper;
using FeatureBasic.src.Shared.Abstractions;

namespace FeatureBasic.src.Features.User
{
    public class UserController(IUserRepository repository, Mapper mapper) : EntityControllerBase<UserEntity>(repository, mapper)
    {
        // Add your specific methods here.
    }
}