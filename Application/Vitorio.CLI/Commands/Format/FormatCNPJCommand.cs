using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Format;

public class FormatCNPJCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> cnpj = new("cnpj", "CNPJ to be formatted");
        Option<bool> remove = new(["--remove", "-r"], () => false, "Remove CNPJ formatting");

        Command command = new("cnpj", "Formats a CNPJ with standard punctuation")
        {
            cnpj,
            remove
        };

        command.SetHandler((string cnpj, bool remove, IConsole console) =>
        {
            if (string.IsNullOrWhiteSpace(cnpj))
            {
                console.Error.WriteLine("CNPJ cannot be empty");
                return;
            }
            if (remove)
                console.Out.WriteLine(Cnpj.RemoveFormat(cnpj));
            else
            {
                if (!Cnpj.IsCnpj(cnpj))
                {
                    console.Error.WriteLine("CNPJ must be a sequence of 14 digits");
                    return;
                }
                else
                    console.Out.WriteLine(Cnpj.Format(cnpj));
            }
        }, cnpj, remove);

        return command;
    }
}
