using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Format;

public class FormatCNPJCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> cnpj = new("cnpj", "CNPJ a ser formatado");
        Option<bool> remove = new(new string[] { "--remove", "-r" }, () => false, "Remove a formatação do CNPJ");

        Command command = new("cnpj", "Formata um CNPJ com a pontuação padrão")
        {
            cnpj,
            remove
        };

        command.SetHandler((string cnpj, bool remove, IConsole console) =>
        {
            if (string.IsNullOrWhiteSpace(cnpj))
            {
                console.Error.WriteLine("cnpj não pode ser vazio");
                return;
            }
            if (remove)
                console.Out.WriteLine(Cnpj.RemoveFormat(cnpj));
            else
            {
                if (!Cnpj.IsCnpj(cnpj))
                {
                    console.Error.WriteLine("cnpj precisa ser uma sequência de 14 digitos");
                    return;
                }
                else
                    console.Out.WriteLine(Cnpj.Foramt(cnpj));
            }
        }, cnpj, remove);

        return command;
    }
}
