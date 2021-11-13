using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Format;

public class FormatCNPJCommand : ICommandFactory
{
    public Command Create()
    {
        Command command = new("cnpj", "Formata um CNPJ com a pontuação padrão")
        {
            new Argument<string>("cnpj", "CNPJ a ser formatado"),
            new Option<bool>(new string[] { "--remove", "-r" }, () => false, "Remove a formatação do CNPJ")
        };

        command.Handler = CommandHandler.Create((string cnpj, bool remove, IConsole console) =>
        {
            if (string.IsNullOrWhiteSpace(cnpj))
            {
                console.Error.WriteLine("cnpj não pode ser vazio");
                return 1;
            }
            if (remove)
                console.Out.WriteLine(Cnpj.RemoveFormat(cnpj));
            else
            {
                if (!Cnpj.IsCnpj(cnpj))
                {
                    console.Error.WriteLine("cnpj precisa ser uma sequência de 14 digitos");
                    return 1;
                }
                else
                    console.Out.WriteLine(Cnpj.Foramt(cnpj));
            }
            return 0;
        });

        return command;
    }
}
