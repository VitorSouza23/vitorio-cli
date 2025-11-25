using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen;

public class GenPasswordCommand : ICommandFactory
{
    public Command Create()
    {
        Option<int> length = new("--length", "-l")
        {
            Description = "Length of the password (Min: 3, Max: 16)",
            DefaultValueFactory = _ => 8
        };
        Option<int> count = new("--count", "-c")
        {
            Description = "Number of passwords to be generated",
            DefaultValueFactory = _ => Count.Default().Value
        };

        Command command = new("password", "Generate password with random characters")
        {
            length,
            count
        };

        command.SetAction(parseResult =>
        {
            var lengthValue = parseResult.GetValue(length);
            var countValue = parseResult.GetValue(count);

            if (((Count)countValue).IsItNotOnRange())
            {
                Console.Error.WriteLine(((Count)countValue).GetNotInRangeMessage());
                return;
            }

            Password password = new(new Random(), lengthValue);

            if (!password.IsLengthInRange())
            {
                Console.Error.WriteLine(Password.GetLengthOutOfRangeMessage());
                return;
            }

            for (int index = 0; index < countValue; index++)
            {
                Console.WriteLine(password.New());
            }
        });

        return command;
    }
}
