using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen;

public class GenCNPJCommand : ICommandFactory
{
    public Command Create()
    {
        Option<bool> formatted = new("--formatted", "-f")
        {
            Description = "Generate CNPJ with punctuation",
            DefaultValueFactory = _ => false
        };
        Option<int> count = new("--count", "-c")
        {
            Description = "Number of CNPJs to be generated",
            DefaultValueFactory = _ => Count.Default().Value
        };

        Command command = new("cnpj", "Generates valid CNPJ")
        {
            formatted,
            count
        };

        command.SetAction(parseResult =>
        {
            var formattedValue = parseResult.GetValue(formatted);
            var countValue = parseResult.GetValue(count);

            if (((Count)countValue).IsItNotOnRange())
            {
                Console.Error.WriteLine(((Count)countValue).GetNotInRangeMessage());
                return;
            }

            Random random = new();
            for (int index = 0; index < countValue; index++)
            {
                Cnpj cnpj = new(random);
                if (formattedValue)
                    cnpj = cnpj.Format();
                Console.WriteLine(cnpj);
            }
        });

        return command;
    }
}
