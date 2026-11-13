using System.Text;
using System.Text.Json;

namespace Vitorio.CLI.Commands.Validate;

public class ValidateJsonCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> input = new("input")
        {
            Arity = ArgumentArity.ZeroOrOne,
            Description = "The JSON string to be validated (reads from stdin when omitted)"
        };

        Command command = new("json", "Validates JSON syntax");
        command.Arguments.Add(input);

        command.SetAction(parseResult =>
        {
            string inputValue = JsonInputNormalizer.Normalize(parseResult.GetValue(input) ?? Console.In.ReadToEnd());
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                Console.Error.WriteLine("JSON input cannot be empty.");
                return 1;
            }

            try
            {
                using JsonDocument document = JsonDocument.Parse(inputValue);
                Console.WriteLine($"Valid JSON. Root type: {GetRootType(document.RootElement.ValueKind)}.");
                return 0;
            }
            catch (JsonException exception)
            {
                WriteSyntaxError(inputValue, exception);
                return 1;
            }
        });

        return command;
    }

    private static string GetRootType(JsonValueKind valueKind) => valueKind switch
    {
        JsonValueKind.Object => "object",
        JsonValueKind.Array => "array",
        JsonValueKind.String => "string",
        JsonValueKind.Number => "number",
        JsonValueKind.True or JsonValueKind.False => "boolean",
        JsonValueKind.Null => "null",
        _ => "unknown"
    };

    private static void WriteSyntaxError(string input, JsonException exception)
    {
        string[] lines = input.Split('\n');
        int lineIndex = (int)Math.Clamp(exception.LineNumber ?? 0, 0, lines.Length - 1);
        string line = lines[lineIndex].TrimEnd('\r');
        long bytePosition = Math.Max(0, exception.BytePositionInLine ?? 0);
        byte[] lineBytes = Encoding.UTF8.GetBytes(line);
        int byteColumn = (int)Math.Min(bytePosition, lineBytes.Length);
        int characterColumn = Encoding.UTF8.GetCharCount(lineBytes, 0, byteColumn);

        const int excerptLength = 120;
        int excerptStart = Math.Max(0, characterColumn - excerptLength / 2);
        int excerptEnd = Math.Min(line.Length, excerptStart + excerptLength);
        string excerpt = line[excerptStart..excerptEnd];
        int prefixLength = excerptStart > 0 ? 3 : 0;
        int suffixLength = excerptEnd < line.Length ? 3 : 0;
        int excerptColumn = characterColumn - excerptStart + prefixLength;

        Console.Error.WriteLine($"Invalid JSON at line {lineIndex + 1}, byte {bytePosition + 1}: {exception.Message}");
        Console.Error.WriteLine($"    {(excerptStart > 0 ? "..." : string.Empty)}{excerpt}{(suffixLength > 0 ? "..." : string.Empty)}");
        Console.Error.WriteLine($"    {new string(' ', excerptColumn)}^");
    }
}
