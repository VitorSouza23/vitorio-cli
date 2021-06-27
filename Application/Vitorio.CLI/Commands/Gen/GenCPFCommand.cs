using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen
{
    public class GenCPFCommand : ICommandFactory
    {
        public Command Create()
        {
            Command command = new("cpf", "Gera CPF válido")
            {
                new Option<bool>(new string[] { "--formated", "-f" }, () => false, "Gera CPF com pontuação"),
                new Option<Count>(new string[] { "--count", "-c" }, () => Count.Default(), "Número de CPFs a serem gerados")
            };

            command.Handler = CommandHandler.Create((bool formated, Count count, IConsole console) =>
            {
                if (count.IsItNotOnRange())
                {
                    console.Error.WriteLine(count.GetNotInRangeMessage());
                    return 1;
                }

                Random random = new();
                for (int index = 0; index < count; index++)
                {
                    Cpf cpf = new Cpf(random).New();
                    if (formated)
                        cpf = cpf.Format();
                    console.Out.WriteLine(cpf);
                }

                return 0;
            });

            return command;
        }
    }
}