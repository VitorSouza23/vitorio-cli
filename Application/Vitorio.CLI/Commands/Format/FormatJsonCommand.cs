using System.Text.Json;

namespace Vitorio.CLI.Commands.Format;

public class FormatJsonCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> input = new("input")
        {
            Arity = ArgumentArity.ZeroOrOne,
            Description = "The JSON string to be formatted (reads from stdin when omitted)"
        };

        Command command = new("json", "Formats a JSON string with indentation")
        {
            input
        };

        command.SetAction(parseResult =>
        {
            var inputValue = JsonInputNormalizer.Normalize(parseResult.GetValue(input) ?? Console.In.ReadToEnd());
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                Console.Error.WriteLine("JSON input cannot be empty.");
                return 1;
            }

            try
            {
                using JsonDocument document = JsonDocument.Parse(inputValue);
                string formattedJson = JsonSerializer.Serialize(document.RootElement, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    IndentSize = 4
                });
                Console.WriteLine(formattedJson);
                return 0;
            }
            catch (JsonException exception)
            {
                Console.Error.WriteLine($"Invalid JSON: {exception.Message}");
                return 1;
            }
        });

        return command;
    }
}
