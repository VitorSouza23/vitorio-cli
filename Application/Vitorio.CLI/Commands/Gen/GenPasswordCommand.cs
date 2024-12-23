using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen;

public class GenPasswordCommand : ICommandFactory
{
    public Command Create()
    {
        Option<int> length = new(["--length", "-l"], () => 8, "Number of password characters (Min: 3, Max: 16)");
        Option<int> count = new(["--count", "-c"], () => Count.Default().Value, "Number of passwords to be generated");

        Command command = new("password", "Generate password with random characters")
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
