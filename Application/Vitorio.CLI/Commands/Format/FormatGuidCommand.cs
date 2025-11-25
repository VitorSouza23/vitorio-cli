
namespace Vitorio.CLI.Commands.Format;

public class FormatGuidCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> guid = new("guid")
        {
            Description = "The GUID to be formatted"
        };
        Option<string> format = new("--format", "-f")
        {
            Description = "The format specifier (N, D, B, P or X)",
            DefaultValueFactory = _ => "D"
        };

        Command command = new("guid", "Formats a GUID according to the specified format")
        {
            guid,
            format
        };

        command.SetAction(parseResult =>
        {
            var guidValue = parseResult.GetValue(guid);
            var formatValue = parseResult.GetValue(format);
            if (string.IsNullOrWhiteSpace(guidValue))
            {
                Console.Error.WriteLine("GUID cannot be empty");
                return;
            }
            if (!Guid.TryParse(guidValue, out Guid parsedGuid))
            {
                Console.Error.WriteLine("Invalid GUID format");
                return;
            }
            try
            {
                string formattedGuid = parsedGuid.ToString(formatValue);
                Console.WriteLine(formattedGuid);
            }
            catch
            {
                Console.Error.WriteLine("Invalid format specifier. Use N, D, B, P or X.");
            }
        });

        return command;
    }
}
