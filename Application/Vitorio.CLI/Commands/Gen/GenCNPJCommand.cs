using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace Vitorio.CLI.Commands.Gen
{
    public class GenCNPJCommand : ICommandFactory
    {
        public Command Create()
        {
            Command command = new("cnpj", "Generete new CNPJ")
            {
                new Option<bool>(new string[] { "--formated", "-f" }, () => false, "Generate a CPF with formatation"),
                new Option<int>(new string[] { "--count", "-c" }, () => 1, "Count of CPF to generate")
            };

            command.Handler = CommandHandler.Create((bool formated, int count, IConsole console) =>
            {
                if (count <= 0)
                {
                    console.Error.Write("--count must be more than zero\n");
                    return 1;
                }

                if (count > 1000)
                {
                    console.Error.Write("--count must be less or equal than 1000\n");
                    return 1;
                }

                for (int index = 0; index < count; index++)
                {
                    string cnpj = GenerateCNPJ();
                    if (formated)
                        cnpj = FormatCNPJ(cnpj);
                    console.Out.Write($"{cnpj}\n");
                }

                return 0;
            });

            return command;
        }

        private string FormatCNPJ(string cnpj)
        {
            const string mask = @"00\.000\.000\/0000\-00";
            return Convert.ToUInt64(cnpj).ToString(mask);
        }

        private string GenerateCNPJ()
        {
            int[] multiplier1 = new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new();
            string seed = rnd.Next(10000000, 99999999).ToString();

            seed += "0001";

            int result = 0;
            for (int index = 0; index < multiplier1.Length; index++)
                result += Convert.ToInt32(seed[index]) * multiplier1[index];

            int rest = result % 11;
            int digit1 = rest < 2 ? 0 : 11 - rest;

            seed += digit1;

            int[] multiplies2 = new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            result = 0;
            for (int index = 0; index < multiplies2.Length; index++)
                result = Convert.ToInt32(seed[index] * multiplies2[index]);

            rest = result % 11;
            int digit2 = rest < 2 ? 0 : 11 - rest;

            seed += digit2;

            return seed;
        }
    }
}