namespace Vitorio.CLI.Commands.Format;

public class FormatStringCommand : ICommandFactory
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

        Command command = new("string", "Formats strings according to a pattern")
        {
            input,
            upper,
            lower
        };

        command.SetAction(parseResult =>
        {
            var inputValue = parseResult.GetValue(input);
            var upperValue = parseResult.GetValue(upper);
            var lowerValue = parseResult.GetValue(lower);
            if (upperValue)
                Console.WriteLine(inputValue.ToUpper());

            if (lowerValue)
                Console.WriteLine(inputValue.ToLower());

            if (!upperValue && !lowerValue)
                Console.Error.WriteLine("No format type selected.");

        });

        command.Subcommands.Add(new FormatGitBranchNameCommand().Create());
        command.Subcommands.Add(new FormatStringListCommand().Create());

        return command;
    }
}