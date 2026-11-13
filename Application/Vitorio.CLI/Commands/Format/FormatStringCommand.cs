using System.Text.RegularExpressions;

namespace Vitorio.CLI.Commands.Format;

public partial class FormatStringCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> input = new("input")
        {
            Description = "The string to be formatted"
        };
        Option<bool> upper = new("--upper", "-u")
        {
            Description = "Formats the string in uppercase",
            DefaultValueFactory = _ => false
        };
        Option<bool> lower = new("--lower", "-l")
        {
            DefaultValueFactory = _ => false,
            Description = "Formats the string in lowercase"
        };
        Option<bool> camelCase = new("--camel-case", "-c")
        {
            DefaultValueFactory = _ => false,
            Description = "Formats the string in camelCase"
        };
        Option<bool> snakeCase = new("--snake-case", "-s")
        {
            DefaultValueFactory = _ => false,
            Description = "Formats the string in snake_case"
        };
        Option<bool> pascalCase = new("--pascal-case", "-p")
        {
            DefaultValueFactory = _ => false,
            Description = "Formats the string in PascalCase"
        };
        Option<bool> normalCase = new("--normal-case", "-n")
        {
            DefaultValueFactory = _ => false,
            Description = "Converts camelCase, snake_case, or PascalCase to a space-separated string"
        };

        Command command = new("string", "Formats strings according to a pattern")
        {
            input,
            upper,
            lower,
            camelCase,
            snakeCase,
            pascalCase,
            normalCase
        };

        command.SetAction(parseResult =>
        {
            var inputValue = parseResult.GetValue(input);
            var upperValue = parseResult.GetValue(upper);
            var lowerValue = parseResult.GetValue(lower);
            var camelCaseValue = parseResult.GetValue(camelCase);
            var snakeCaseValue = parseResult.GetValue(snakeCase);
            var pascalCaseValue = parseResult.GetValue(pascalCase);
            var normalCaseValue = parseResult.GetValue(normalCase);
            bool formatSelected = false;

            if (upperValue)
            {
                Console.WriteLine(inputValue.ToUpper());
                formatSelected = true;
            }

            if (lowerValue)
            {
                Console.WriteLine(inputValue.ToLower());

                formatSelected = true;
            }

            if (camelCaseValue)
            {
                Console.WriteLine(ToCamelCase(inputValue));
                formatSelected = true;
            }

            if (snakeCaseValue)
            {
                Console.WriteLine(ToSnakeCase(inputValue));
                formatSelected = true;
            }

            if (pascalCaseValue)
            {
                Console.WriteLine(ToPascalCase(inputValue));
                formatSelected = true;
            }

            if (normalCaseValue)
            {
                Console.WriteLine(ToNormalCase(inputValue));
                formatSelected = true;
            }

            if (!formatSelected)
                Console.Error.WriteLine("No format type selected.");

        });

        command.Subcommands.Add(new FormatGitBranchNameCommand().Create());
        command.Subcommands.Add(new FormatStringListCommand().Create());

        return command;
    }

    private static string ToCamelCase(string input)
    {
        string[] words = GetWords(input);
        return words.Length == 0
            ? string.Empty
            : string.Concat(words[0].ToLowerInvariant(), string.Concat(words.Skip(1).Select(ToTitleCase)));
    }

    private static string ToSnakeCase(string input) =>
        string.Join("_", GetWords(input).Select(word => word.ToLowerInvariant()));

    private static string ToPascalCase(string input) =>
        string.Concat(GetWords(input).Select(ToTitleCase));

    private static string ToNormalCase(string input) =>
        string.Join(" ", GetWords(input).Select(word => word.ToLowerInvariant()));

    private static string[] GetWords(string input)
    {
        string separatedWords = SeparateWordsRegex.Replace(input, " ");

        return [.. SplitWordsRegex.Split(separatedWords).Where(word => word.Length > 0)];
    }

    private static string ToTitleCase(string word) =>
        string.Concat(char.ToUpperInvariant(word[0]), word[1..].ToLowerInvariant());

    [GeneratedRegex(@"[^\p{L}\p{N}]+")]
    private static partial Regex SplitWordsRegex { get; }

    [GeneratedRegex(@"(?<=[\p{Ll}\p{Nd}])(?=\p{Lu})|(?<=\p{Lu})(?=\p{Lu}\p{Ll})")]
    private static partial Regex SeparateWordsRegex { get; }
}
