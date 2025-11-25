using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen;

public class GenCEPCommand : ICommandFactory
{
    public Command Create()
    {
        Option<bool> formatted = new("--formatted", "-f")
        {
            Description = "Generates CEP with punctuation",
            DefaultValueFactory = _ => false
        };
        Option<int> count = new("--count", "-c")
        {
            Description = "Number of CEPs to be generated",
            DefaultValueFactory = _ => Count.Default().Value
        };

        Command command = new("cep", "Generates CEPs (valid or not)")
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
                CEP cep = new(random);
                if (formattedValue)
                    cep = cep.Format();
                Console.WriteLine(cep);
            }
        });

        return command;
    }
}
