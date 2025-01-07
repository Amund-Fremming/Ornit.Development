using FeatureResult.src.Features.Auth;
using FeatureResult.src.Features.Example;
using FeatureResult.src.Shared.Common;

namespace FeatureResult.src.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddLibraries(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(typeof(AutoMapperDtoProfile));
        }

        public static void AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITokenService, TokenService>();
        }

        public static void AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IExampleRepository, ExampleRepository>();
        }
    }
}