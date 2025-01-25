using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text;

namespace Ornit.Backend.src.Shared.Builders;

public static class TypeScriptClientGenerator
{
    public static void Generate()
    {
        var controllerClasses = GetControllerClasses();
        // Convert all this to a big linq
        foreach (var controllerClass in controllerClasses)
        {
            var methods = GetCustomMethods(controllerClass);
            foreach (var method in methods)
            {
                var textClient = ToTextClient(method);
            }
        }
    }

    private static string ToTextClient(MethodInfo method)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"const {method.Name} = () => {"{"}");

        return sb.ToString();
    }

    private static IEnumerable<Type> GetControllerClasses()
        => Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false }
                && typeof(ControllerBase).IsAssignableFrom(t));

    private static MethodInfo[] GetCustomMethods(Type controllerClass)
        => controllerClass.GetMethods(
            BindingFlags.Public |
            BindingFlags.Instance |
            BindingFlags.DeclaredOnly);

    /*
     * Psuedo
     * - get all types
     * - foreach type
     *  - select ( to text client )
     * - foreach text client
     *  - CreateClient
     * - foreach type
     *  - get url base
     *  - select ( to endpoint
     */

    // GetControllerTypes
    // ToTextClient()
    // ToUrlBase
}