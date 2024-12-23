using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Format;

public sealed class FormatGitBranchNameCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> input = new("input", "An entry that will be formatted with the Git branch name pattern");
        Option<string> prefix = new(["--prefix", "-p"], () => string.Empty, "Prefix that will be added to the branch name separated by '/'");

        Command command = new("git-branch", "Takes a text input and formats it following the Git branch naming standard")
        {
            input,
            prefix
        };

        command.SetHandler((string input, string prefix, IConsole console) =>
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                console.Error.WriteLine("Input cannot be empty");
                return;
            }

            if (input.Length > 100)
            {
                console.Error.WriteLine("Input cannot be longer than 100 characters");
                return;
            }

            if (string.IsNullOrWhiteSpace(prefix) is false && prefix.Length > 20)
            {
                console.Error.WriteLine("Prefix cannot be longer than 20 characters");
                return;
            }

            console.Out.WriteLine(GitBranchName.Format(input, prefix));

        }, input, prefix);

        return command;
    }
}
