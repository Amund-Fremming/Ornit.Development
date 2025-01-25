using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text;
using static Ornit.Backend.src.Shared.TypeScript.TypeScriptCommon;

namespace Ornit.Backend.src.Shared.TypeScript;

/*
 * INFO
 *
 * ToTextClient
 * - Needs testing
 * - Do we want to throw on not 200 response or return some message
 *
 * General
 * - Maybe add a flag to say if its development build or production build
 * - Then we can remove the logging and throwing errors to try and hide information
 *
 */

public static class TypeScriptClientGenerator
{
    public static void Generate()
    {
        try
        {
            var controllerClasses = GetControllerClasses();
            // Convert all this to a big linq

            var textClients = controllerClasses
                .Select(c => GetCustomMethods(c)
                       .Select(ToTextClient)
                       .Aggregate(string.Empty, string.Concat));
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static string ToTextClient(MethodInfo method)
    {
        try
        {
            // NEEDS FIX
            var endpointBase = "TODO:ENDPOINT_BASE";
            var methodEndpoint = "TODO:METHOD_ENDPOINT";
            var httpMethod = "TODO:HTTPMETHOD";
            var authorization = true ? $"Authorization: \"Bearer {{token}}\"" : "";

            var sb = new StringBuilder();
            var methodName = ToCamelCase(method.Name);
            var methodParams = method.GetParameters()
                .OfType<ParameterInfo>()
                .Select(ToTypeScriptParameter)
                .Aggregate(string.Empty, string.Concat);

            sb.AppendLine($$"""
			const {{methodName}} = async ({{methodParams}}) => {
				try {
					const response = await fetch(`{{endpointBase}}/{{methodEndpoint}}`, {
						method: "{{httpMethod}}",
						headers: {
							"Content-Type": "application/json",
							{{authorization}}
						}
					});

					if (!response.ok) {
						const errorMessage = await response.json();
						throw new Error(errorMessage);
					}

					const data = await response.json();
					return data;
				} catch (error) {
					console.log("{{methodName}} error: " + error.message);
				}
			};
		""");
            sb.AppendLine();

            return sb.ToString();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static string ToTypeScriptParameter(ParameterInfo paramInfo)
    {
        try
        {
            var space = " ";
            var paramTsType = ToTypeScriptType(paramInfo.ParameterType);
            var result = string.Concat(paramTsType, space, paramInfo.Name);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
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
}