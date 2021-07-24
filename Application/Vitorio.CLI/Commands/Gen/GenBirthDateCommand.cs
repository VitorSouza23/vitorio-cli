using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands
{
    public class GenBirthDateCommand : ICommandFactory
    {
        public Command Create()
        {
            Command command = new("birthdate", "Gera a data de aniversário respecitiva a uma idade")
            {
                new Argument<uint>("age", "Idade da qual será estraída a data de aniversário")
            };

            command.Handler = CommandHandler.Create((uint age, IConsole console) =>
            {
                try
                {
                    BirthDate birthDate = new();
                    console.Out.WriteLine(birthDate.ByAge((int)age));
                    return 0;
                }
                catch (Exception)
                {
                    console.Error.WriteLine("Valor de 'age' inválido para geração de data");
                    return 1;
                }
            });

            return command;
        }
    }
}