using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Format;

public sealed class FormatGitBranchNameCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> input = new("input", "Uma entreda que será formatada com o padrão de nome de branch do Git");
        Option<string> prefix = new(new string[] { "--prefix", "-p" }, () => string.Empty, "Prefixo que será adicionado ao nome da bracnh separado po '/'");

        Command command = new("git-branch", "Recebe uma entrada de texto e formata seguindo o padrão de nomes de branch do Git")
        {
            input,
            prefix
        };

        command.SetHandler((string input, string prefix, IConsole console) =>
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                console.Error.WriteLine("Input não pode ser vazio");
                return;
            }

            if (input.Length > 100)
            {
                console.Error.WriteLine("Input não pode ter mais que 100 caracteres");
                return;
            }

            if (string.IsNullOrWhiteSpace(prefix) is false && prefix.Length > 20)
            {
                console.Error.WriteLine("Prefixo não pode ter mais que 20 caracteres");
                return;
            }

            console.Out.WriteLine(GitBranchName.Format(input, prefix));

        }, input, prefix);

        return command;
    }
}
