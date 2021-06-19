using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using Vitorio.CLI.Extensions;

namespace Vitorio.CLI.Commands.Gen
{
    public class GenCNPJCommand : ICommandFactory
    {
        public Command Create()
        {
            Command command = new("cnpj", "Gera CNPJ válido")
            {
                new Option<bool>(new string[] { "--formated", "-f" }, () => false, "Gera CNPJ com pontuação"),
                new Option<int>(new string[] { "--count", "-c" }, () => 1, "Número de CNPJs a serem gerados")
            };

            command.Handler = CommandHandler.Create((bool formated, int count, IConsole console) =>
            {
                if (count <= 0)
                {
                    console.Error.WriteLine("--count deve ser maior que zero");
                    return 1;
                }

                if (count > 1000)
                {
                    console.Error.WriteLine("--count deve ser menor que 1000");
                    return 1;
                }

                for (int index = 0; index < count; index++)
                {
                    string cnpj = GenerateCNPJ();
                    if (formated)
                        cnpj = cnpj.FormatCNPJ();
                    console.Out.WriteLine(cnpj);
                }

                return 0;
            });

            return command;
        }

        private string GenerateCNPJ()
        {
            int[] multiplier1 = new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new();
            string seed = rnd.Next(0, 99999999).ToString("D8");

            seed += "0001";
            seed += CalculateCheckDigit(multiplier1, seed);

            int[] multiplier2 = new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            seed += CalculateCheckDigit(multiplier2, seed);

            return seed;

            int CalculateCheckDigit(int[] multiplier, string seed)
            {
                int result = 0;
                for (int index = 0; index < multiplier.Length; index++)
                    result += (int)char.GetNumericValue(seed[index]) * multiplier[index];

                int rest = result % 11;
                return rest < 2 ? 0 : 11 - rest;
            }
        }
    }
}