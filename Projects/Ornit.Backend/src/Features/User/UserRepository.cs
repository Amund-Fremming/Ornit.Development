using Ornit.Backend.src.Shared.Abstractions;
using Ornit.Backend.src.Shared.AppData;

namespace Ornit.Backend.src.Features.User
{
    public class UserRepository(ILogger<UserRepository> logger, AppDbContext context) : RepositoryBase<UserEntity, UserRepository>(logger, context), IUserRepository
    {
    }
}