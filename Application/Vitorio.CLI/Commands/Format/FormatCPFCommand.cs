using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Format;

public class FormatCPFCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> cpf = new("cpf", "CPF to be formatted");
        Option<bool> remove = new(["--remove", "-r"], () => false, "Remove CPF formatting");

        Command command = new("cpf", "Formats a CPF with standard punctuation")
        {
            cpf,
            remove
        };

        command.SetHandler((string cpf, bool remove, IConsole console) =>
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                console.Error.WriteLine("CPF cannot be empty");
                return;
            }
            if (remove)
                console.Out.WriteLine(Cpf.RemoveFormat(cpf));
            else
            {
                if (!Cpf.IsCPF(cpf))
                {
                    console.Error.WriteLine("CPF must be a sequence of 11 digits");
                    return;
                }
                else
                    console.Out.WriteLine(Cpf.Format(cpf));
            }
        }, cpf, remove);

        return command;
    }
}
