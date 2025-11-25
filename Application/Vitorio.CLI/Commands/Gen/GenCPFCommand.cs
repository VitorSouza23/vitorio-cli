using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen;

public class GenCPFCommand : ICommandFactory
{
    public Command Create()
    {
        Option<bool> formatted = new("--formatted", "-f")
        {
            Description = "Generate CPF with punctuation",
            DefaultValueFactory = _ => false
        };
        Option<int> count = new("--count", "-c")
        {
            Description = "Number of CPFs to be generated",
            DefaultValueFactory = _ => Count.Default().Value
        };

        Command command = new("cpf", "Generates valid CPF")
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
                Cpf cpf = new(random);
                if (formattedValue)
                    cpf = cpf.Format();
                Console.WriteLine(cpf);
            }
        });

        return command;
    }
}
