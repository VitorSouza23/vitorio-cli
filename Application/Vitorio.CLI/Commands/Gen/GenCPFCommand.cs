using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using Vitorio.CLI.Extensions;

namespace Vitorio.CLI.Commands.Gen
{
    public class GenCPFCommand : ICommandFactory
    {
        public Command Create()
        {
            Command command = new("cpf", "Gera CPF válido")
            {
                new Option<bool>(new string[] { "--formated", "-f" }, () => false, "Gera CPF com pontuação"),
                new Option<int>(new string[] { "--count", "-c" }, () => 1, "Número de CPFs a serem gerados")
            };

            command.Handler = CommandHandler.Create((bool formated, int count, IConsole console) =>
            {
                if (count <= 0)
                {
                    console.Error.Write("--count deve ser maioir que zero\n");
                    return 1;
                }

                if (count > 1000)
                {
                    console.Error.Write("--count deve ser menor que 1000\n");
                    return 1;
                }

                for (int index = 0; index < count; index++)
                {
                    string cpf = GenerateCPF();
                    if (formated)
                        cpf = cpf.FormatCPF();
                    console.Out.Write($"{cpf}\n");
                }

                return 0;
            });

            return command;
        }

        private string GenerateCPF()
        {
            int[] multiplier1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new();
            string seed = rnd.Next(0, 999999999).ToString("D9");

            seed += CalculateCheckDigit(multiplier1, seed);

            int[] multiplier2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            seed += CalculateCheckDigit(multiplier2, seed);

            return seed;

            int CalculateCheckDigit(int[] multiplier, string seed)
            {
                int remainder = 0, sum = 0;

                for (int index = 0; index < multiplier.Length; index++)
                    sum += int.Parse(seed[index].ToString()) * multiplier[index];

                remainder = sum % 11;
                if (remainder < 2)
                    remainder = 0;
                else
                    remainder = 11 - remainder;

                return remainder;
            }
        }
    }
}