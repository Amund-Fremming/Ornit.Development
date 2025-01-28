using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Ornit.Backend.src.Shared.Abstractions;
using System.Reflection;
using System.Text;
using static Ornit.Backend.src.Shared.TypeScript.TypeScriptCommon;

namespace Ornit.Backend.src.Shared.TypeScript;

/*
 * TODO
 *
 * - FEATURE: add a flag to say if its development build or production build, so we can remove logs
 * - FEATURE: Do we want to throw on not 200 response or return some message
 *
 * - FIX: add params to body or param
 * - FIX: client needs to specify the data returned ??
 *
 * - BUG: Dictionary<T, T> does not work as param
 *
 * - ADD: Sending data in params
 * - ADD: Sending data in body
 * - ADD: Adding token in params if authorization
 */

public static class TypeScriptClientGenerator
{
    public static void Generate()
    {
        try
        {
            var controllerClasses = GetControllerClasses();
            var textClients = controllerClasses
                .Select(GetCustomMethods)
                .Select(ToTextClient)
                .ToList();

            for (int i = 0; i < controllerClasses.Count(); i++)
            {
                var controllerName = controllerClasses.ElementAt(i).Name;
                var textClient = textClients.ElementAt(i);
                CreatedFile(controllerName, textClient);
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

    private static string ToTextClient(MethodInfo[] methodInfos)
    {
        try
        {
            var tsImports = new StringBuilder();
            tsImports.Append("import { ");
            var objects = new HashSet<string>();

            var tsFunctions = new StringBuilder();
            foreach (var method in methodInfos)
            {
                var methodObjects = GetCustomObjects(method.GetParameters());
                objects.UnionWith(methodObjects);

                var needsAuth = MethodNeedsAuth(method);
                var clientMethodText = GetClientMethodString(method, needsAuth);
                tsFunctions.AppendLine(clientMethodText);
            }

            var allTypes = string.Join(", ", objects);
            tsImports.AppendLine($"{allTypes} }} from \"../contenttypes\";" + "\n");
            tsImports.Append(tsFunctions);

            return tsImports.ToString();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static HashSet<string> GetCustomObjects(ParameterInfo[] parameterInfos)
    {
        var objects = new HashSet<string>();
        foreach (var param in parameterInfos)
        {
            var type = param.ParameterType;

            if (ImplementsTypeScript(type))
            {
                objects.Add(type.Name);
                continue;
            }

            if (type.IsArray)
            {
                objects.Add(type.GetElementType()!.Name);
                continue;
            }

            if (type.IsGenericType)
            {
                foreach (var arg in type.GetGenericArguments())
                {
                    objects.Add(arg.Name);
                }
            }
        }

        return objects;
    }

    private static bool ImplementsTypeScript(Type type) => typeof(ITypeScriptModel).IsAssignableFrom(type);

    private static string GetEndpointBase(MethodInfo method)
    {
        var type = method.DeclaringType!;

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

    private static string GetClientMethodString(MethodInfo method, bool needsAuth)
        => $$"""
			const {{ToCamelCase(method.Name)}} = async ({{GetMethodParams(method, needsAuth)}}) => {
				try {
					const response = await fetch(`{{GetEndpointBase(method)}}/{{GetMethodEndpoint(method)}}`, {
						method: "{{GetHttpVerb(method.GetCustomAttributes())}}",
						headers: {
							"Content-Type": "application/json",
							{{GetAuthorizationHeaders(needsAuth)}}
						}
					});

					if (!response.ok) {
						const errorMessage = await response.json();
						throw new Error(errorMessage);
					}

					const data = await response.json();
					return data;
				} catch (error) {
					console.log("{{method.Name}} error: " + error.message);
				}
			};
		""";

    private static string GetAuthorizationHeaders(bool needsAuth) => needsAuth ? $"Authorization: `Bearer ${{token}}`" : "";

    private static bool MethodNeedsAuth(MethodInfo method) => method.GetCustomAttributes().OfType<AuthorizeAttribute>().Any();

    private static string GetMethodParams(MethodInfo method, bool needsAuth)
    {
        var parameters = method.GetParameters()
            .OfType<ParameterInfo>()
            .Select(ToTypeScriptParameter)
            .Aggregate(string.Empty, string.Concat);

        if (!needsAuth)
        {
            return parameters;
        }

        return string.IsNullOrEmpty(parameters) ? "token: string" : string.Concat(parameters, ", token: string");
    }

    private static string GetMethodEndpoint(MethodInfo method)
        => (method.GetCustomAttributes()
               .FirstOrDefault(a => a is HttpMethodAttribute) as HttpMethodAttribute)?
               .Template ?? "";
}