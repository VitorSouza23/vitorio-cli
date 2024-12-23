using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands;

public class GenCEPCommand : ICommandFactory
{
    public Command Create()
    {
        Option<bool> formatted = new(["--formatted", "-f"], () => false, "Generates CEP with punctuation");
        Option<int> count = new(["--count", "-c"], () => Count.Default().Value, "Number of CEPs to be generated");

        Command command = new("cep", "Generates CEPs (valid or not)")
        {
            formatted,
            count
        };

        command.SetHandler((bool formatted, int count, IConsole console) =>
        {
            if (((Count)count).IsItNotOnRange())
            {
                console.Error.WriteLine(((Count)count).GetNotInRangeMessage());
                return;
            }

            Random random = new();

            for (int index = 0; index < count; index++)
            {
                CEP cep = new(random);
                if (formatted)
                    cep = cep.Format();
                console.Out.WriteLine(cep);
            }
        }, formatted, count);

        return command;
    }
}
