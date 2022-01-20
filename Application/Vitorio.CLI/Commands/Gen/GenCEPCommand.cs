using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands;

public class GenCEPCommand : ICommandFactory
{
    public Command Create()
    {
        Option<bool> formated = new(new string[] { "--formated", "-f" }, () => false, "Gera CEP com pontuação");
        Option<int> count = new(new string[] { "--count", "-c" }, () => Count.Default().Value, "Número de CEPs a serem gerados");

        Command command = new Command("cep", "Gera CEPs (válidos ou não)")
        {
            formated,
            count
        };

        command.SetHandler((bool formated, int count, IConsole console) =>
        {
            if (((Count)count).IsItNotOnRange())
            {
                console.Error.WriteLine(((Count)count).GetNotInRangeMessage());
                return;
            }

            Random random = new();

            for (int index = 0; index < count; index++)
            {
                CEP cep = new CEP(random).New();
                if (formated)
                    cep = cep.Format();
                console.Out.WriteLine(cep);
            }
        }, formated, count);

        return command;
    }
}
