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
            var assembly = Assembly.GetExecutingAssembly();
            var models = assembly.GetTypes()
                .Where(t => t is { IsClass: true, IsAbstract: false } && typeof(ITypeScriptModel).IsAssignableFrom(t));

            StringBuilder sb = new();
            foreach (var model in models)
            {
                sb.AppendLine($"export type\"{{\"");
                foreach (var property in model.GetProperties())
                {
                    var tsPropertyName = ToCamelCase(property.Name);
                    var tsType = ToTypeScriptType(property.GetType());
                    sb.AppendLine($"    {tsType}: {tsPropertyName};");
                }
                sb.AppendLine("\"{\"");
                sb.AppendLine();
            }

            string fullFile = sb.ToString();
        }

        private static string ToTypeScriptType(object value)
        {
            return value switch
            {
                int number => nameof(number),
                string text => "string",
                bool boolean => nameof(boolean),
                char c => "char",
                // Enumerable
                // List
                // Dict
                // Objects
                _ => throw new InvalidOperationException("Type does not exist")
            };
        }

        private static string ToCamelCase(string name) => name[0..1].ToLower() + name[1..];
    }
}