using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen;

public class GenPasswordCommand : ICommandFactory
{
    public Command Create()
    {
        Option<int> length = new(new string[] { "--length", "-l" }, () => 8, "Número de caracteres da senha (Min: 3, Max: 16)");
        Option<int> count = new(new string[] { "--count", "-c" }, () => Count.Default().Value, "Número de senhas a serem geradas");

        Command command = new("password", "Gera senha com caracteres aleatórios")
        {
            length,
            count
        };

        command.SetHandler((int length, int count, IConsole console) =>
        {
            if (((Count)count).IsItNotOnRange())
            {
                console.Error.WriteLine(((Count)count).GetNotInRangeMessage());
                return;
            }

            Password password = new(new Random(), length);

            if (!password.IsLengthInRange())
            {
                console.Error.WriteLine(Password.GetLengthOutOfRangeMessage());
                return;
            }

            for (int index = 0; index < count; index++)
            {
                console.Out.WriteLine(password.New());
            }
        }, length, count);

        return command;
    }
}
