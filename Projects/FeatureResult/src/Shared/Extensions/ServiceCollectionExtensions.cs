﻿using FeatureResult.src.Features.User;
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
        }

        public static void AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
        }
    }
}