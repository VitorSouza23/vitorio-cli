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
                new Option<int>(new string[] { "--count", "-c" }, () => 1, "Quantidade de números telefônicos a serem gerados"),
                new Option<bool>(new string[] { "--not-formated", "-nf" }, () => false, "Não formata o número telefônico")
            };

            command.Handler = CommandHandler.Create((PhoneRules phoneRules, int count, IConsole console) =>
            {
                if (phoneRules.NumberOfDigits is < 3 or > 9)
                {
                    console.Error.WriteLine("--number-of-digits deve ser entre 3 a 10");
                    return 1;
                }

                if (count < 0)
                {
                    console.Error.WriteLine("--count deve ser maior que zero");
                    return 1;
                }

                if (count > 1000)
                {
                    console.Error.WriteLine("--count deve ser menor que 1000");
                    return 1;
                }

                Phone phone = new(new Random());
                for (int index = 0; index < count; index++)
                {
                    console.Out.WriteLine(phone.New(phoneRules));
                }

                return 0;
            });

            return command;
        }
    }
}