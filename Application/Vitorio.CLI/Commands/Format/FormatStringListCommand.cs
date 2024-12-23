using System.CommandLine.IO;
using System.Text;

namespace Vitorio.CLI.Commands.Format;

public class FormatStringListCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> input = new("input", "A list of strings to be formatted");
        Option<string> separator = new(["--separator", "-sp"], () => "\n", "The separator for each list item");
        Option<string> prefix = new(["--prefix", "-p"], () => string.Empty, "Prefix to be placed on all items in the list");
        Option<string> suffix = new(["--suffix", "-s"], () => string.Empty, "Suffix to be placed on all items in the list");

        Command command = new("list", "Formats the items in a list of strings according to a pattern")
        {
            input,
            separator,
            prefix,
            suffix
        };

        command.SetHandler((string input, string separator, string prefix, string suffix, IConsole console) =>
        {
            try
            {
                string[] strings = input.Split(separator);
                StringBuilder sb = new();
                foreach (string value in strings)
                {
                    sb.AppendLine($"{prefix}{value}{suffix}");
                }
                string result = sb.ToString();
                console.Out.WriteLine(result);
            }
            catch
            {
                console.Error.WriteLine("Error trying to format list of strings");
            }
        }, input, separator, prefix, suffix);

        return command;
    }
}