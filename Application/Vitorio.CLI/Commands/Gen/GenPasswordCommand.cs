using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.Linq;
using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen
{
    public class GenPasswordCommand : ICommandFactory
    {
        public Command Create()
        {
            Command command = new("password", "Gera senha com caracteres aleatórios")
            {
                new Option<int>(new string[] { "--length", "-l" }, () => 8, "Número de caracteres da senha (Min: 3, Max: 16)"),
                new Option<Count>(new string[] { "--count", "-c" }, () => Count.Default(), "Número de senhas a serem geradas")
            };

            command.Handler = CommandHandler.Create((int length, Count count, IConsole console) =>
            {
                if (count.IsItNotOnRange())
                {
                    console.Error.WriteLine(count.GetNotInRangeMessage());
                    return 1;
                }

                Password password = new(new Random(), length);

                if (!password.IsLengthInRange())
                {
                    console.Error.WriteLine(password.GetLengthOutOfRangeMessage());
                    return 1;
                }

                for (int index = 0; index < count; index++)
                {
                    console.Out.WriteLine(password.New());
                }

                return 0;
            });

            return command;
        }
    }
}