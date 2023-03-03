using System.CommandLine.IO;
using System.Text;

namespace Vitorio.CLI.Commands.Format;

public class FormatStringListCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> input = new("input", "Uma lista de strings a ser formatada");
        Option<string> separator = new(new string[] { "--separator", "-sp" }, () => "\n", "O separador de cada item da lista");
        Option<string> prefix = new(new string[] { "--prefix", "-p" }, () => string.Empty, "Prefixo a ser colocado em todos os itens da lista");
        Option<string> suffix = new(new string[] { "--suffix", "-s" }, () => string.Empty, "Sufixo a ser colocado em todos os itens da lista");

        Command command = new("list", "Formata os itens de uma lista de strings de acordo com um padrão")
        {
            input,
            separator,
            prefix,
            suffix
        };

        command.SetHandler((string input, string separator, string prefix, string suffix, IConsole console) =>
        {
            try
            {
                string[] strings = input.Split(separator);
                StringBuilder sb = new();
                foreach (string value in strings)
                {
                    sb.AppendLine($"{prefix}{value}{suffix}");
                }
                string result = sb.ToString();
                console.Out.WriteLine(result);
            }
            catch
            {
                console.Error.WriteLine("Erro ao tentar formatar lista de strings");
            }
        }, input, separator, prefix, suffix);

        return command;
    }
}