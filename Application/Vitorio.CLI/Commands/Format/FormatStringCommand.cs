using System.CommandLine;
using System.CommandLine.IO;

namespace Vitorio.CLI.Commands.Format;

public class FormatStringCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> input = new("input", "The string to be formatted");
        Option<bool> upper = new(["--upper", "-u"], () => false, "Formats the string in uppercase");
        Option<bool> lower = new(["--lower", "-l"], () => false, "Formats the string in lowercase");

        Command command = new("string", "Formats strings according to a pattern")
        {
            input,
            upper,
            lower
        };

        command.SetHandler((string input, bool upper, bool lower, IConsole console) =>
        {
            if (upper)
                console.Out.WriteLine(input.ToUpper());

            if (lower)
                console.Out.WriteLine(input.ToLower());

            if (!upper && !lower)
                console.Error.WriteLine("No format type selected.");

        }, input, upper, lower);

        command.AddCommand(new FormatGitBranchNameCommand().Create());
        command.AddCommand(new FormatStringListCommand().Create());

        return command;
    }
}