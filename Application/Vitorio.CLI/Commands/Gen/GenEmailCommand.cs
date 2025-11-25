using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen;

public class GenEmailCommand : ICommandFactory
{
    public Command Create()
    {
        Option<string> provider = new("--provider", "-p")
        {
            Description = "Email provider (e.g.: gmail, yahoo, outlook, etc.)"
        };
        Option<string> domain = new("--domain", "-d")
        {
            Description = "Email domain (e.g.: com, com.br, etc.)",
            DefaultValueFactory = _ => "com"
        };
        Option<int> count = new("--count", "-c")
        {
            Description = "Number of emails to be generated",
            DefaultValueFactory = _ => Count.Default().Value
        };

        Command command = new("email", "Generates email address (not necessarily valid)")
        {
            provider,
            domain,
            count
        };

        command.SetAction(parseResult =>
        {
            var providerValue = parseResult.GetValue(provider);
            var domainValue = parseResult.GetValue(domain);
            var countValue = parseResult.GetValue(count);

            if (((Count)countValue).IsItNotOnRange())
            {
                Console.Error.WriteLine(((Count)countValue).GetNotInRangeMessage());
                return;
            }

            Random random = new();
            for (int index = 0; index < countValue; index++)
            {
                Console.WriteLine(new Email(random, providerValue, domainValue));
            }
        });

        return command;
    }
}
