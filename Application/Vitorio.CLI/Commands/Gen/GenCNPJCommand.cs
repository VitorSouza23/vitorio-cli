using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen;

public class GenCNPJCommand : ICommandFactory
{
    public Command Create()
    {
        Option<bool> formatted = new(["--formatted", "-f"], () => false, "Generate CNPJ with punctuation");
        Option<int> count = new(["--count", "-c"], () => Count.Default().Value, "Number of CNPJs to be generated");

        Command command = new("cnpj", "Generates valid CNPJ")
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
                Cnpj cnpj = new(random);
                if (formatted)
                    cnpj = cnpj.Format();
                console.Out.WriteLine(cnpj);
            }
        }, formatted, count);

        return command;
    }
}
