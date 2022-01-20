using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen;

public class GenEmailCommand : ICommandFactory
{
    public Command Create()
    {
        Option<string> provider = new(new string[] { "--provider", "-p" }, "Provedor de e-mail personalizado");
        Option<string> domain = new(new string[] { "--domain", "-d" }, () => "com", "Domínio do e-mail (Ex: com, com.br, etc.)");
        Option<int> count = new(new string[] { "--count", "-c" }, () => Count.Default().Value, "Número de e-mails a serem gerados");

        Command command = new("email", "Gera endereço de e-mail (não necessariamente válido)")
        {
            provider,
            domain,
            count
        };

        command.SetHandler((string provider, string domain, int count, IConsole console) =>
        {
            if (((Count)count).IsItNotOnRange())
            {
                console.Error.WriteLine(((Count)count).GetNotInRangeMessage());
                return;
            }

            Random random = new();
            for (int index = 0; index < count; index++)
            {
                console.Out.WriteLine(new Email(random, provider, domain));
            }
        }, provider, domain, count);

        return command;
    }
}
