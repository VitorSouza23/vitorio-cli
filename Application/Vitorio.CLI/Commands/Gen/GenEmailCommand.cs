using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen;

public class GenEmailCommand : ICommandFactory
{
    public Command Create()
    {
        Option<string> provider = new(["--provider", "-p"], "Custom Email Provider");
        Option<string> domain = new(["--domain", "-d"], () => "com", "Email domain (e.g.: com, com.br, etc.)");
        Option<int> count = new(["--count", "-c"], () => Count.Default().Value, "Number of emails to be generated");

        Command command = new("email", "Generates email address (not necessarily valid)")
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
