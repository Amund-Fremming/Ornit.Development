using FeatureBasic.src.Shared.Abstractions;
using FeatureBasic.src.Shared.AppData;

namespace FeatureBasic.src.Features.User
{
    public class UserRepository(ILogger<UserRepository> logger, AppDbContext context) : RepositoryBase<UserEntity, UserRepository>(logger, context), IUserRepository
    {
    }
}