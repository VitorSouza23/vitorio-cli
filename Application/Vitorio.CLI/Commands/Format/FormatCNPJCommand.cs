using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Format;

public class FormatCNPJCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> cnpj = new("cnpj")
        {
            Description = "CNPJ to be formatted"
        };
        Option<bool> remove = new("--remove", "-r")
        {
            Description = "Remove CNPJ formatting",
            DefaultValueFactory = _ => false
        };

        Command command = new("cnpj", "Formats a CNPJ with standard punctuation")
        {
            cnpj,
            remove
        };

        command.SetAction(parseResult =>
        {
            var cnpjValue = parseResult.GetValue(cnpj);
            var removeValue = parseResult.GetValue(remove);
            if (string.IsNullOrWhiteSpace(cnpjValue))
            {
                Console.Error.WriteLine("CNPJ cannot be empty");
                return;
            }
            if (removeValue)
                Console.WriteLine(Cnpj.RemoveFormat(cnpjValue));
            else
            {
                if (!Cnpj.IsCnpj(cnpjValue))
                {
                    Console.Error.WriteLine("CNPJ must be a sequence of 14 digits");
                    return;
                }
                else
                    Console.WriteLine(Cnpj.Format(cnpjValue));
            }
        });

        return command;
    }
}
