using Ornit.Backend.src.Features.Auth0;
using Ornit.Backend.src.Features.User;
using Ornit.Backend.src.Shared.Common;

namespace Ornit.Backend.src.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddLibraries(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(typeof(AutoMapperDtoProfile));
        }

        public static void AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddScoped<IImageProcessorClient, ImageProcessorClient>();
        }

        public static void AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
        }
    }
}