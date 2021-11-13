using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Format;

public class FormatCPFCommand : ICommandFactory
{
    public Command Create()
    {
        Command command = new("cpf", "Formata um CPF com a pontuação padrão")
        {
            new Argument<string>("cpf", "CPF a ser formatado"),
            new Option<bool>(new string[] { "--remove", "-r" }, () => false, "Remove a formatação do CPF")
        };

        command.Handler = CommandHandler.Create((string cpf, bool remove, IConsole console) =>
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                console.Error.WriteLine("cpf não pode ser vazio");
                return 1;
            }
            if (remove)
                console.Out.WriteLine(Cpf.RemoveFormat(cpf));
            else
            {
                if (!Cpf.IsCPF(cpf))
                {
                    console.Error.WriteLine("cpf precisa ser uma sequência de 11 digitos");
                    return 1;
                }
                else
                    console.Out.WriteLine(Cpf.Format(cpf));
            }
            return 0;
        });

        return command;
    }
}
