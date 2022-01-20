using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen;

public class GenCNPJCommand : ICommandFactory
{
    public Command Create()
    {
        Option<bool> formated = new(new string[] { "--formated", "-f" }, () => false, "Gera CNPJ com pontuação");
        Option<int> count = new(new string[] { "--count", "-c" }, () => Count.Default().Value, "Número de CNPJs a serem gerados");

        Command command = new("cnpj", "Gera CNPJ válido")
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
                Cnpj cnpj = new Cnpj(random).New();
                if (formated)
                    cnpj = cnpj.Format();
                console.Out.WriteLine(cnpj);
            }
        }, formated, count);

        return command;
    }
}
