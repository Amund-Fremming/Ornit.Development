using Ornit.Backend.src.Shared.Abstractions;
using System.Reflection;
using System.Text;
using static Ornit.Backend.src.Shared.TypeScript.TypeScriptCommon;

namespace Ornit.Backend.src.Shared.TypeScript;

public static class TypeScriptTypeGenerator
{
    public const string _filename = "contenttypes.ts";

    public static void Generate()
    {
        var types = GetTypeScriptModelTypes();
        var fileContent = GenerateFileContent(types);
        WriteToFile(fileContent);
    }

    private static IEnumerable<Type> GetTypeScriptModelTypes()
        => Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false }
                && typeof(ITypeScriptModel).IsAssignableFrom(t));

    // Maybe write to frontend folder not in backend folder?
    private static void WriteToFile(string fileContent)
    {
        var directory = Directory.GetCurrentDirectory();
        var newDir = directory + "\\" + _filename;
        File.WriteAllText(newDir, fileContent);
    }

    private static string GenerateFileContent(IEnumerable<Type> types)
    {
        StringBuilder sb = new();
        foreach (var type in types)
        {
            sb.AppendLine($"export interface {type.Name} {"{"}");
            foreach (var property in type.GetProperties())
            {
                var tsPropertyName = ToCamelCase(property.Name);
                var tsType = ToTypeScriptType(property.PropertyType);
                sb.AppendLine($"    {tsPropertyName}: {tsType};");
            }
            sb.AppendLine($"{"}"}");
            sb.AppendLine();
        }

        return sb.ToString();
    }
}