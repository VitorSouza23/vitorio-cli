using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands;

public class GenPhoneCommand : ICommandFactory
{

    public Command Create()
    {
        Command command = new("phone", "Gera número de telefone")
        {
            new Option<int>(new string[] { "--code-country", "-cc" }, () => 0, "Código do país"),
            new Option<int>(new string[] { "--ddd", "-d" }, () => 0, "Código DDD da localidade de destino"),
            new Option<int>(new string[] { "--number-of-digits", "-nd" }, () => 9, "Quantidade de dígitos no número (Min = 3, Max = 9)"),
            new Option<int>(new string[] { "--count", "-c" }, () => Count.Default().Value, "Quantidade de números telefônicos a serem gerados"),
            new Option<bool>(new string[] { "--not-formated", "-nf" }, () => false, "Não formata o número telefônico")
        };

        command.Handler = CommandHandler.Create((PhoneRules phoneRules, int count, IConsole console) =>
        {
            if (((Count)count).IsItNotOnRange())
            {
                console.Error.WriteLine(((Count)count).GetNotInRangeMessage());
                return 1;
            }

            Phone phone = new(new Random(), phoneRules);

            if (phone.IsNotNumberOfDigitsInRange())
            {
                console.Error.WriteLine(phone.GetNotInRangeMessage());
                return 1;
            }

            for (int index = 0; index < count; index++)
            {
                console.Out.WriteLine(phone.New());
            }

            return 0;
        });

        return command;
    }
}
