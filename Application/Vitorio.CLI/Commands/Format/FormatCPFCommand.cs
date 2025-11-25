using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Format;

public class FormatCPFCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> cpf = new("cpf")
        {
            Description = "The CPF number to be formatted"
        };
        Option<bool> remove = new("--remove", "-r")
        {
            Description = "Remove CPF formatting",
            DefaultValueFactory = _ => false
        };

        Command command = new("cpf", "Formats a CPF with standard punctuation")
        {
            cpf,
            remove
        };

        command.SetAction(parseResult =>
        {
            var cpfValue = parseResult.GetValue(cpf);
            var removeValue = parseResult.GetValue(remove);
            if (string.IsNullOrWhiteSpace(cpfValue))
            {
                Console.Error.WriteLine("CPF cannot be empty");
                return;
            }
            if (removeValue)
                Console.WriteLine(Cpf.RemoveFormat(cpfValue));
            else
            {
                if (!Cpf.IsCPF(cpfValue))
                {
                    Console.Error.WriteLine("CPF must be a sequence of 11 digits");
                    return;
                }
                else
                    Console.WriteLine(Cpf.Format(cpfValue));
            }
        });

        return command;
    }
}
