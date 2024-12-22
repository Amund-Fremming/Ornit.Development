using FeatureBasic.src.Features.Data;
using FeatureBasic.src.Shared.Abstractions;

namespace FeatureBasic.src.Features.User
{
    public class UserRepository(ILogger<UserRepository> logger, AppDbContext context) : RepositoryBase<UserEntity, UserRepository>(logger, context)
    {
    }
}