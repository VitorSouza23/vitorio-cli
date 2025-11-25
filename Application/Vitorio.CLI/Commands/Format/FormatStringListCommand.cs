using System.Text;

namespace Vitorio.CLI.Commands.Format;

public class FormatStringListCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> input = new("input"){
            Description = "A list of strings to be formatted"
        };
        Option<string> separator = new("--separator", "-sp"){
            Description = "The separator for each list item",
            DefaultValueFactory = _ => "\n"
        };
        Option<string> prefix = new("--prefix", "-p"){
            Description = "Prefix to be placed on all items in the list",
            DefaultValueFactory = _ => string.Empty
        };
        Option<string> suffix = new("--suffix", "-s"){
            Description = "Suffix to be placed on all items in the list",
            DefaultValueFactory = _ => string.Empty
        };

        Command command = new("list", "Formats the items in a list of strings according to a pattern")
        {
            input,
            separator,
            prefix,
            suffix
        };

        command.SetAction(parseResult =>
        {
            var inputValue = parseResult.GetValue(input);
            var separatorValue = parseResult.GetValue(separator);
            var prefixValue = parseResult.GetValue(prefix);
            var suffixValue = parseResult.GetValue(suffix);

            try
            {
                string[] strings = inputValue.Split(separatorValue);
                StringBuilder sb = new();
                foreach (string value in strings)
                {
                    sb.AppendLine($"{prefix}{value}{suffix}");
                }
                string result = sb.ToString();
                Console.WriteLine(result);
            }
            catch
            {
                Console.Error.WriteLine("Error trying to format list of strings");
            }
        });

        return command;
    }
}