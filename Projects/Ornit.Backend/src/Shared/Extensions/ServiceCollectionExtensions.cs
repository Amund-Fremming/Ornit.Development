using Ornit.Backend.src.Features.Auth0;
using Ornit.Backend.src.Features.User;
using Ornit.Backend.src.Shared.Image;

namespace Ornit.Backend.src.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLibraries(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IImageHandler, ImageHandler>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}