using Ornit.Backend.src.Shared.Abstractions;
using System.Reflection;
using System.Text;

namespace Ornit.Backend.src.Shared.Builders
{
    public class TypeScriptTypeGenerator
    {
        public const string _filename = "contenttypes.ts";

        public static void Create()
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

        // Make prettier
        private static string ToTypeScriptType(Type type)
        {
            if (type == typeof(int) ||
                type == typeof(uint) ||
                type == typeof(ulong) ||
                type == typeof(long) ||
                type == typeof(float) ||
                type == typeof(double) ||
                type == typeof(decimal)) return "number";
            if (type == typeof(string) || type == typeof(char)) return "string";
            if (type == typeof(bool)) return "boolean";
            if (type.IsArray) return ToTypeScriptType(type.GetElementType()!) + "[]";

            if (type.IsGenericType)
            {
                var genericType = type.GetGenericTypeDefinition();

                if (genericType == typeof(List<>) ||
                    genericType == typeof(IList<>) ||
                    genericType == typeof(IEnumerable<>))
                {
                    return ToTypeScriptType(type.GetGenericArguments()[0]) + "[]";
                }

                if (genericType == typeof(Dictionary<,>) || genericType == typeof(IDictionary<,>))
                {
                    var key = ToTypeScriptType(type.GetGenericArguments()[0]);
                    var value = ToTypeScriptType(type.GetGenericArguments()[1]);
                    return $"Map<{key}, {value}>";
                }

                if (genericType == typeof(HashSet<>) ||
                    genericType == typeof(ISet<>))
                {
                    var value = ToTypeScriptType(type.GetGenericArguments()[0]);
                    return $"Set<{value}>";
                }
            }

            if (typeof(ITypeScriptModel).IsAssignableFrom(type))
            {
                var name = type.Name;
                return name;
            }

            return "any";
        }

        private static string ToCamelCase(string name) => name[0..1].ToLower() + name[1..];
    }
}