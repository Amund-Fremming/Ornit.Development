namespace Ornit.Backend.src.Shared.TypeScript;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTypeScriptSupport(this IServiceCollection services, Action<TypeScriptGenerationOptions>? configureOptions)
    {
        var options = new TypeScriptGenerationOptions();
        configureOptions?.Invoke(options);

        TypeScriptTypeGenerator.Generate();
        TypeScriptClientGenerator.Generate(options.ClientLogging);

        return services;
    }
}