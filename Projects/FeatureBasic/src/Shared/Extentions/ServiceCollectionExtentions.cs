using FeatureBasic.src.Shared.Common;

namespace FeatureBasic.src.Shared.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddLibraries(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(typeof(AutoMapperProfile));
        }

        public static void AddServices(this IServiceCollection serviceCollection)
        {
            // serviceCollection.AddScoped<>;
        }

        public static void AddRepositories(this IServiceCollection serviceCollection)
        {
            // serviceCollection.AddScoped<>;
        }
    }
}