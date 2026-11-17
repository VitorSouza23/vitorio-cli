using System.Text.Json;

namespace Vitorio.CLI.Commands;

internal static class JsonInputNormalizer
{
    public static string Normalize(string input)
    {
        string normalizedInput = input.Trim();

        if (normalizedInput.Length > 2
            && normalizedInput[0] == '"'
            && (normalizedInput[1] == '{' || normalizedInput[1] == '[')
            && normalizedInput.Contains("\\\"", StringComparison.Ordinal))
        {
            try
            {
                var unwrappedInput = JsonSerializer.Deserialize<string>(normalizedInput);
                if (unwrappedInput is not null)
                    return unwrappedInput;
            }
            catch (JsonException)
            {
                return normalizedInput.Replace("\\\"", "\"", StringComparison.Ordinal);
            }
        }

        try
        {
            using JsonDocument document = JsonDocument.Parse(normalizedInput);
            return normalizedInput;
        }
        catch (JsonException)
        {
            return normalizedInput.Replace("\\\"", "\"", StringComparison.Ordinal);
        }
    }
}
