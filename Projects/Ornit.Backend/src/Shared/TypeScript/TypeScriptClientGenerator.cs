using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
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

            var textClients = controllerClasses
                .Select(cls => GetCustomMethods(cls)
                       .Select(method => ToTextClient(method, cls))
                       .Aggregate(string.Empty, string.Concat));

            for (int i = 0; i < controllerClasses.Count(); i++)
            {
                CreatedFile(controllerClasses.ElementAt(i).Name, textClients.ElementAt(i));
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static void CreatedFile(string controllerName, string content)
    {
        controllerName = controllerName.Replace("Controller", "Api.ts");
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "api", controllerName);
        Directory.CreateDirectory("api");
        File.WriteAllText(filePath, content);
    }

    /*
     * Needs
     * - Imports in files
     * - Sending params in body or in url
     */

    private static string ToTextClient(MethodInfo method, Type type)
    {
        try
        {
            var endpointBase = GetEndpointBase(type);

            var methodEndpoint = (method.GetCustomAttributes()
                .FirstOrDefault(a => a is HttpMethodAttribute) as HttpMethodAttribute)?
                .Template ?? "";

            var httpMethod = GetHttpVerb(method.GetCustomAttributes());
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

    private static string GetEndpointBase(Type type)
    {
        var endpointTemplateBase = type.GetCustomAttribute<RouteAttribute>()?
            .Template ?? "";

        var controllerName = type.Name.Replace("Controller", string.Empty);
        var endpointBase = endpointTemplateBase.Replace("[controller]", controllerName);

        return endpointBase;
    }

    private static object GetHttpVerb(IEnumerable<Attribute> attributes)
        => attributes switch
        {
            _ when attributes.OfType<HttpGetAttribute>().Any() => "GET",
            _ when attributes.OfType<HttpPostAttribute>().Any() => "POST",
            _ when attributes.OfType<HttpPutAttribute>().Any() => "PUT",
            _ when attributes.OfType<HttpDeleteAttribute>().Any() => "DELETE",
            _ when attributes.OfType<HttpPatchAttribute>().Any() => "PATCH",
            _ => "GET"
        };

    private static string ToTypeScriptParameter(ParameterInfo paramInfo)
    {
        try
        {
            var space = ": ";
            var paramTsType = ToTypeScriptType(paramInfo.ParameterType);
            var result = string.Concat(paramInfo.Name, space, paramTsType);
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