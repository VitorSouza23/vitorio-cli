using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.Text;
using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands
{
    public class GenPhoneCommand : ICommandFactory
    {

        public Command Create()
        {
            Command command = new("phone", "Gera número de telefone")
            {
                new Option<int>(new string[] { "--code-country", "-cc" }, () => 0, "Código do país"),
                new Option<int>(new string[] { "--ddd", "-d" }, () => 0, "Código DDD da localidade de destino"),
                new Option<int>(new string[] { "--number-of-digits", "-nd" }, () => 9, "Quantidade de dígitos no número (Min = 3, Max = 9)"),
                new Option<Count>(new string[] { "--count", "-c" }, () => Count.Default(), "Quantidade de números telefônicos a serem gerados"),
                new Option<bool>(new string[] { "--not-formated", "-nf" }, () => false, "Não formata o número telefônico")
            };

            command.Handler = CommandHandler.Create((PhoneRules phoneRules, Count count, IConsole console) =>
            {
                if (count.IsItNotOnRange())
                {
                    console.Error.WriteLine(count.GetNotInRangeMessage());
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
}