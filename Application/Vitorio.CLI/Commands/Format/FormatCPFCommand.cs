using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Format;

public class FormatCPFCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> cpf = new("cpf", "CPF a ser formatado");
        Option<bool> remove = new(new string[] { "--remove", "-r" }, () => false, "Remove a formatação do CPF");

        Command command = new("cpf", "Formata um CPF com a pontuação padrão")
        {
            cpf,
            remove
        };

        command.SetHandler((string cpf, bool remove, IConsole console) =>
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                console.Error.WriteLine("cpf não pode ser vazio");
                return;
            }
            if (remove)
                console.Out.WriteLine(Cpf.RemoveFormat(cpf));
            else
            {
                if (!Cpf.IsCPF(cpf))
                {
                    console.Error.WriteLine("cpf precisa ser uma sequência de 11 digitos");
                    return;
                }
                else
                    console.Out.WriteLine(Cpf.Format(cpf));
            }
        }, cpf, remove);

        return command;
    }
}
