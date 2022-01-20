using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen;

public class GenCPFCommand : ICommandFactory
{
    public Command Create()
    {
        Option<bool> formated = new(new string[] { "--formated", "-f" }, () => false, "Gera CPF com pontuação");
        Option<int> count = new(new string[] { "--count", "-c" }, () => Count.Default().Value, "Número de CPFs a serem gerados");

        Command command = new("cpf", "Gera CPF válido")
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
                Cpf cpf = new Cpf(random).New();
                if (formated)
                    cpf = cpf.Format();
                console.Out.WriteLine(cpf);
            }
        }, formated, count);

        return command;
    }
}
