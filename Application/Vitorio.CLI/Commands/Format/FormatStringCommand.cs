using System.CommandLine;
using System.CommandLine.IO;

namespace Vitorio.CLI.Commands.Format;

public class FormatStringCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> input = new("input", "A string a ser formatada");
        Option<bool> upper = new(new string[] { "--upper", "-u" }, () => false, "Formata a string em caixa alta");
        Option<bool> lower = new(new string[] { "--lower", "-l" }, () => false, "Formata a string em letras minúsculas");

        Command command = new("string", "Formata strings de acordo com um padrão")
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
                console.Error.WriteLine("Nenhum tipo de formato selecionado.");

        }, input, upper, lower);

        command.AddCommand(new FormatGitBranchNameCommand().Create());
        command.AddCommand(new FormatStringListCommand().Create());

        return command;
    }
}