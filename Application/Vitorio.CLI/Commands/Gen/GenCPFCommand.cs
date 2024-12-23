using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen;

public class GenCPFCommand : ICommandFactory
{
    public Command Create()
    {
        Option<bool> formatted = new(["--formatted", "-f"], () => false, "Generate CPF with punctuation");
        Option<int> count = new(["--count", "-c"], () => Count.Default().Value, "Number of CPFs to be generated");

        Command command = new("cpf", "Generates valid CPF")
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
                Cpf cpf = new(random);
                if (formatted)
                    cpf = cpf.Format();
                console.Out.WriteLine(cpf);
            }
        }, formatted, count);

        return command;
    }
}
