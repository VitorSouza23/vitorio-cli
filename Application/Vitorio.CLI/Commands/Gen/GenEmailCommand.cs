using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.Linq;

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
                new Option<int>(new string[] { "--count", "-c" }, () => 1, "Número de e-mails a serem gerados")
            };

            command.Handler = CommandHandler.Create((string provider, string domain, int count, IConsole console) =>
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

                Random random = new();
                for (int index = 0; index < count; index++)
                {
                    console.Out.WriteLine(GenerateEmail(provider, domain, random));
                }

                return 0;
            });

            return command;
        }

        public string GenerateEmail(string provider, string domain, Random random)
        {
            const int randomStringsLength = 5;
            string thisProvider = string.IsNullOrWhiteSpace(provider) ?
                RandomString(random, randomStringsLength) :
                provider;

            string thisDomain = string.IsNullOrWhiteSpace(domain) ?
                "com" :
                domain;

            return $"{RandomString(random, randomStringsLength)}@{thisProvider}.{thisDomain}";

        }

        private string RandomString(Random random, int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}