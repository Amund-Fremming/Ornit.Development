using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;
using System.Text;
using static Ornit.Backend.src.Shared.TypeScript.TypeScriptCommon;

namespace Ornit.Backend.src.Shared.TypeScript;

/*
 * TODO
 *
 * - Return some message when 200, 401, 403, 404 and 500 (Result pattern or TanStack)
 * - Client needs to specify the data returned
 */

public static class TypeScriptClientGenerator
{
    private static bool ClientLogging = false;

    public static void Generate(bool clientLogging)
    {
        try
        {
            ClientLogging = clientLogging;

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

                var clientMethodText = GetClientMethodString(method);
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

            if (IsCustomObjectType(type))
            {
                objects.Add(type.Name);
                continue;
            }

            if (type.IsArray && IsCustomObjectType(type))
            {
                objects.Add(type.GetElementType()!.Name);
                continue;
            }

            if (type.IsEnum)
            {
                objects.Add(type.Name);
                continue;
            }

            if (type.IsGenericType)
            {
                objects.UnionWith(type.GetGenericArguments()
                    .Where(IsCustomObjectType)
                    .Select(t => t.Name));
            }
        }

        return objects;
    }

    private static string GetEndpointBase(MethodInfo method)
    {
        var type = method.DeclaringType!;

        var endpointTemplateBase = type.GetCustomAttribute<RouteAttribute>()?
            .Template ?? "";

        var controllerName = type.Name.Replace("Controller", string.Empty);
        var endpointBase = endpointTemplateBase.Replace("[controller]", controllerName);

        return endpointBase;
    }

    private static string GetHttpVerb(IEnumerable<Attribute> attributes)
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

    private static string GetClientMethodString(MethodInfo method)
    {
        var needsAuth = method.GetCustomAttributes()
            .OfType<AuthorizeAttribute>()
            .Any();

        var methodName = ToCamelCase(method.Name);
        var typeScriptParams = GetTypeScriptParams(method, needsAuth);

        var endpointBase = GetEndpointBase(method);
        var methodUri = GetMethodEndpoint(method);

        foreach (var param in method.GetParameters().Select(p => p.Name))
        {
            methodUri = methodUri.Replace($"{{{param}}}", $"${{{param}}}");
        }

        var httpVerb = GetHttpVerb(method.GetCustomAttributes());
        var authorization = needsAuth ? $"Authorization: `Bearer ${{token}}`" : "";
        var body = GetBody(method.GetParameters());

        var returnType = GetReturnType(method);
        var catchClauseConsoleLog = ClientLogging ? $"console.log(\"{method.Name} error: \" + error.message);" : "";

        return $$"""
			const {{methodName}} = async ({{typeScriptParams}}) => {
				try {
					const response = await fetch(`{{endpointBase}}/{{methodUri}}}`, {
						method: "{{httpVerb}}",
						headers: {
							"Content-Type": "application/json",
							{{authorization}}
						},
						{{body}}
					});

					if (!response.ok) {
						const errorMessage = await response.json();
						throw new Error(errorMessage);
					}

					const data = await response.json();
					return data;
				} catch (error) {
				    {{catchClauseConsoleLog}}
				}
			};
		""";
    }

    private static string GetReturnType(MethodInfo method)
    {
        throw new NotImplementedException("GetReturnType in TypeScriptGenerator is not implemented yet, stupid.");
    }

    private static string GetBody(ParameterInfo[] parameterInfos)
        => parameterInfos.Where(p => p.GetCustomAttributes().OfType<FromBodyAttribute>().Any())
               .Select(p => $"body: JSON.stringify({p.Name}),")
               .FirstOrDefault() ?? "";

    private static string GetTypeScriptParams(MethodInfo method, bool needsAuth)
    {
        var methodParams = string.Join(", ", method.GetParameters().Select(ToTypeScriptParameter));
        if (needsAuth)
        {
            var tokenString = methodParams.Length == 0 ? "token: string" : ", token: string";
            methodParams = string.Concat(methodParams, tokenString);
        }

        return methodParams;
    }

    private static string GetMethodEndpoint(MethodInfo method)
        => (method.GetCustomAttributes()
               .FirstOrDefault(a => a is HttpMethodAttribute) as HttpMethodAttribute)?
               .Template ?? "";
}