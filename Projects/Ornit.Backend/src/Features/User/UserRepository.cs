using FeatureResult.src.Shared.Abstractions;
using FeatureResult.src.Shared.AppData;

namespace FeatureResult.src.Features.User
{
    public class UserRepository(ILogger<UserRepository> logger, AppDbContext context) : RepositoryBase<UserEntity, UserRepository>(logger, context), IUserRepository
    {
    }
}