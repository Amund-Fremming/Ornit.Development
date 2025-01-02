using Spectre.Console;

namespace TemplateBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var crawler = new Crawler();
            crawler.MoveOut();
            crawler.MoveOut();
            crawler.MoveOut();
            crawler.MoveOut();
            crawler.MoveIn("Projects");

            var projects = GetProjectsToConvert(crawler.CurrentDir);
            crawler.MoveOut();
            crawler.MoveIn("Templates");
            foreach (var project in projects)
            {
                MakeCopy(project);
                var dirs = Directory.GetDirectories(crawler.CurrentDir);
                //  MakeTemplate(result[0]);
            }
        }

        private static void MakeCopy(string project)
        {
        }

        public static List<string> GetProjectsToConvert(string currentDir)
        {
            static string ExtractName(string dir) => dir.Substring(dir.IndexOf("Projects") + 8, dir.Length - dir.IndexOf("Projects") - 8);

            List<string> names = Directory.GetDirectories(currentDir)
                .Select(dir => ExtractName(dir))
                .ToList();

            return AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title("What projects to generate template?")
                    .InstructionsText("(Press <space> to toggle, <enter> to accept)")
                    .AddChoices(names));
        }
    }
}