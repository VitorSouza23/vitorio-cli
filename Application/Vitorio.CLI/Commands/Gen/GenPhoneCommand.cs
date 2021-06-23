using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.Text;

namespace Vitorio.CLI.Commands
{
    public class GenPhoneCommand : ICommandFactory
    {
        public Command Create()
        {
            Command command = new("phone", "Gera número de telefone")
            {
                new Option<int>(new string[] { "--country-code", "-cc" }, () => 0, "Código do país"),
                new Option<int>(new string[] { "--ddd", "-d" }, () => 0, "Código DDD da localidade de destino"),
                new Option<int>(new string[] { "--number-of-digits", "-n" }, () => 9, "Quantidade de dígitos no número (Min = 3, Max = 10)"),
                new Option<int>(new string[] { "--count", "-c" }, () => 1, "Quantidade de números telefônicos a serem gerados"),
                new Option<bool>(new string[] { "--formated", "-f" }, () => true, "Formata o número telefônico com máscara xxx-xxxx")
            };

            command.Handler = CommandHandler.Create((int countryCode, int ddd, int numberOfDigits, int count, bool formated, IConsole console) =>
            {
                if (numberOfDigits is < 3 or > 10)
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
                    console.Out.WriteLine(GeneratePhoneNumber(countryCode, ddd, numberOfDigits, formated, random));
                }

                return 0;
            });

            return command;
        }

        private string GeneratePhoneNumber(int countryCode, int ddd, int numberOfDigits, bool formated, Random random)
        {
            StringBuilder stringBuilder = new();
            for (int index = 0; index < numberOfDigits; index++)
            {
                stringBuilder.Append("9");
            }
            int maxNumberValue = int.Parse(stringBuilder.ToString());
            string phoneNumber = random.Next(0, maxNumberValue).ToString($"D{numberOfDigits}");

            stringBuilder = new();

            if (countryCode > 0)
            {
                stringBuilder.Append(formated ? $"+{countryCode}" : countryCode);
                stringBuilder.Append(" ");
            }

            if (ddd > 0)
            {
                stringBuilder.Append(formated ? $"({ddd})" : ddd);
                stringBuilder.Append(" ");
            }

            if (formated && numberOfDigits >= 7)
                stringBuilder.Append(phoneNumber.Insert(phoneNumber.Length - 4, "-"));
            else
                stringBuilder.Append(phoneNumber);

            return stringBuilder.ToString();
        }
    }
}