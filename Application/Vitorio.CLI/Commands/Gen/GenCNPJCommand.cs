using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen;

public class GenCNPJCommand : ICommandFactory
{
    public Command Create()
    {
        Command command = new("cnpj", "Gera CNPJ válido")
        {
            new Option<bool>(new string[] { "--formated", "-f" }, () => false, "Gera CNPJ com pontuação"),
            new Option<int>(new string[] { "--count", "-c" }, () => Count.Default().Value, "Número de CNPJs a serem gerados")
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
                Cnpj cnpj = new Cnpj(random).New();
                if (formated)
                    cnpj = cnpj.Format();
                console.Out.WriteLine(cnpj);
            }

            return 0;
        });

        return command;
    }
}
