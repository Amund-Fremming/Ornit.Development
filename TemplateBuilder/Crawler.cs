using Spectre.Console;

namespace TemplateBuilder
{
    public class Crawler
    {
        public string CurrentDir { get; private set; }

        public Crawler()
        {
            CurrentDir = Directory.GetCurrentDirectory();
        }

        public void MoveIn(string dir)
        {
            var newDir = Path.Combine(CurrentDir, dir);
            if (Directory.Exists(newDir))
            {
                CurrentDir = newDir;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Directory '{newDir}' does not exist.[/]");
            }
        }

        public void MoveOut()
        {
            string? parentDirectory = Directory.GetParent(CurrentDir)?.FullName;
            if (parentDirectory != null)
            {
                CurrentDir = parentDirectory;
                return;
            }

            AnsiConsole.MarkupLine($"[red]Already at the root directory, can't move out further.[/]");
        }
    }
}
