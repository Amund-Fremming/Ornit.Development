using FeatureBasic.src.Features.User;
using FeatureBasic.src.Shared.Common;

namespace FeatureBasic.src.Shared.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddLibraries(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(typeof(AutoMapperDtoProfile));
        }

        public static void AddServices(this IServiceCollection serviceCollection)
        {
        }

        public static void AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
        }
    }
}