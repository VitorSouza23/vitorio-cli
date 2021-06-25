using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.Linq;

namespace Vitorio.CLI.Commands.Gen
{
    public class GenPasswordCommand : ICommandFactory
    {
        public Command Create()
        {
            Command command = new("password", "Gera senha com caracteres aleatórios")
            {
                new Option<int>(new string[] { "--length", "-l" }, () => 8, "Número de caracteres da senha (Min: 3, Max: 16)"),
                new Option<int>(new string[] { "--count", "-c" }, () => 1, "Número de senhas a serem geradas")
            };

            command.Handler = CommandHandler.Create((int length, int count, IConsole console) =>
            {
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

                if (length is < 3 or > 16)
                {
                    console.Error.WriteLine("--length deve ser maior que 3 e menor que 16");
                    return 1;
                }

                Random random = new();
                for (int index = 0; index < count; index++)
                {
                    console.Out.WriteLine(GeneratePassword(length, random));
                }

                return 0;
            });

            return command;
        }

        private string GeneratePassword(int length, Random random)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%&*^";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}