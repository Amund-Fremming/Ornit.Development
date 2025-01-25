namespace Ornit.Backend.src.Shared.Builders;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTypeScriptSupport(this IServiceCollection services)
    {
        TypeScriptTypeGenerator.Generate();
        TypeScriptClientGenerator.Generate();

        return services;
    }
}