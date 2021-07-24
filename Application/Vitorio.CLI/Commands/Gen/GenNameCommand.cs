using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands
{
    public class GenNameCommand : ICommandFactory
    {
        public Command Create()
        {
            Command command = new("name", "Gera nome aleatório")
            {
                new Option<char>(new string[] { "--gender", "-g" }, () => 'A', "Gera nome feminino (F), mascilino (M) ou aleatório (A)"),
                new Option<int>(new string[] { "--count", "-c" }, () => Count.Default().Value, "Número de nomes a serem gerados")
            };

            command.Handler = CommandHandler.Create((char gender, int count, IConsole console) =>
            {
                if (((Count)count).IsItNotOnRange())
                {
                    console.Error.WriteLine(((Count)count).GetNotInRangeMessage());
                    return 1;
                }

                NameGender nameGender = gender switch
                {
                    'F' or 'f' => NameGender.Feminine,
                    'M' or 'm' => NameGender.Masculine,
                    'A' or 'a' => NameGender.All,
                    _ => NameGender.NotDefined
                };

                if (nameGender == NameGender.NotDefined)
                {
                    console.Error.WriteLine("--gender deve ser 'A' (Todos), 'F' (Feminino) ou 'M' (Masculino)");
                    return 1;
                }

                Name name = new(new Random());
                for (int index = 0; index < count; index++)
                {
                    console.Out.WriteLine(name.New(nameGender));
                }

                return 0;
            });

            return command;
        }
    }
}