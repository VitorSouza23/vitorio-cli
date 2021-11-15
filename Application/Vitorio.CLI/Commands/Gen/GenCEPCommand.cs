using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands;

public class GenCEPCommand : ICommandFactory
{
    public Command Create()
    {
        Command command = new Command("cep", "Gera CEPs (válidos ou não)")
            {
                new Option<bool>(new string[] {"--formated", "-f"}, () => false, "Gera CEP com pontuação"),
                new Option<int>(new string[] { "--count", "-c" }, () => Count.Default().Value, "Número de CEPs a serem gerados")

            };

        command.Handler = CommandHandler.Create((bool formated, int count, IConsole console) =>
        {
            if (((Count)count).IsItNotOnRange())
            {
                console.Error.WriteLine(((Count)count).GetNotInRangeMessage());
                return 1;
            }

            Random random = new();

            for (int index = 0; index < count; index++)
            {
                CEP cep = new CEP(random).New();
                if (formated)
                    cep = cep.Format();
                console.Out.WriteLine(cep);
            }

            return 0;
        });

        return command;
    }
}
