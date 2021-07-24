using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen
{
    public class GenEmailCommand : ICommandFactory
    {
        public Command Create()
        {
            Command command = new("email", "Gera endereço de e-mail (não necessariamente válido)")
            {
                new Option<string>(new string[] { "--provider", "-p" }, "Provedor de e-mail personalizado"),
                new Option<string>(new string[] { "--domain", "-d" }, () => "com", "Domínio do e-mail (Ex: com, com.br, etc.)"),
                new Option<int>(new string[] { "--count", "-c" }, () => Count.Default().Value, "Número de e-mails a serem gerados")
            };

            command.Handler = CommandHandler.Create((string provider, string domain, int count, IConsole console) =>
            {
                if (((Count)count).IsItNotOnRange())
                {
                    console.Error.WriteLine(((Count)count).GetNotInRangeMessage());
                    return 1;
                }

                Random random = new();
                for (int index = 0; index < count; index++)
                {
                    console.Out.WriteLine(new Email(random, provider, domain));
                }

                return 0;
            });

            return command;
        }
    }
}