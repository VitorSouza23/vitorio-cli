using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.Text;

namespace Vitorio.CLI.Commands
{
    public class GenPhoneCommand : ICommandFactory
    {
        internal record PhoneRules(int CodeCountry, int Ddd, int NumberOfDigits, bool NotFormated);
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

                Random random = new();
                for (int index = 0; index < count; index++)
                {
                    console.Out.WriteLine(GeneratePhoneNumber(phoneRules, random));
                }

                return 0;
            });

            return command;
        }

        private string GeneratePhoneNumber(PhoneRules phoneRules, Random random)
        {
            StringBuilder stringBuilder = new();
            for (int index = 0; index < phoneRules.NumberOfDigits; index++)
            {
                stringBuilder.Append("9");
            }
            int maxNumberValue = int.Parse(stringBuilder.ToString());
            string phoneNumber = random.Next(0, maxNumberValue).ToString($"D{phoneRules.NumberOfDigits}");

            stringBuilder = new();

            if (phoneRules.CodeCountry > 0)
            {
                stringBuilder.Append(phoneRules.NotFormated ? phoneRules.CodeCountry : $"+{phoneRules.CodeCountry}");
                stringBuilder.Append(" ");
            }

            if (phoneRules.Ddd > 0)
            {
                stringBuilder.Append(phoneRules.NotFormated ? phoneRules.Ddd : $"({phoneRules.Ddd})");
                stringBuilder.Append(" ");
            }

            if (phoneRules.NotFormated is false && phoneRules.NumberOfDigits >= 7)
                stringBuilder.Append(phoneNumber.Insert(phoneNumber.Length - 4, "-"));
            else
                stringBuilder.Append(phoneNumber);

            return stringBuilder.ToString();
        }
    }
}