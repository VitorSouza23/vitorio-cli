using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Format;

public sealed class FormatGitBranchNameCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> input = new("input")
        {
            Description = "An input string to be formatted as a Git branch name"
        };
        Option<string> prefix = new("--prefix", "-p")
        {
            Description = "Prefix that will be added to the branch name separated by '/'",
            DefaultValueFactory = _ => string.Empty
        };

        Command command = new("git-branch", "Takes a text input and formats it following the Git branch naming standard")
        {
            input,
            prefix
        };

        command.SetAction(parseResult =>
        {
            var inputValue = parseResult.GetValue(input);
            var prefixValue = parseResult.GetValue(prefix);

            if (string.IsNullOrWhiteSpace(inputValue))
            {
                Console.Error.WriteLine("Input cannot be empty");
                return;
            }

            if (inputValue.Length > 100)
            {
                Console.Error.WriteLine("Input cannot be longer than 100 characters");
                return;
            }

            if (string.IsNullOrWhiteSpace(prefixValue) is false && prefixValue.Length > 20)
            {
                Console.Error.WriteLine("Prefix cannot be longer than 20 characters");
                return;
            }

            Console.WriteLine(GitBranchName.Format(inputValue, prefixValue));

        });

        return command;
    }
}
