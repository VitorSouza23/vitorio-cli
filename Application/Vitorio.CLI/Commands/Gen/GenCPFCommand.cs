using System;
using System.CommandLine;
using System.CommandLine.Invocation;

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
                        cpf = FormatCPF(cpf);
                    console.Out.Write($"{cpf}\n");
                }

                return 0;
            });

            return command;
        }

        private string FormatCPF(string cpf)
        {
            const string cpfMask = @"000\.000\.000\-00";
            return Convert.ToUInt64(cpf).ToString(cpfMask);
        }

        private string GenerateCPF()
        {
            int sum = 0, remainder = 0;
            int[] multiplier1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new();
            string seed = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
                sum += int.Parse(seed[i].ToString()) * multiplier1[i];

            remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            seed = seed + remainder;
            sum = 0;

            for (int i = 0; i < 10; i++)
                sum += int.Parse(seed[i].ToString()) * multiplier2[i];

            remainder = sum % 11;

            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            seed = seed + remainder;
            return seed;
        }
    }
}