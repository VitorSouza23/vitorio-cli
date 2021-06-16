using System;
using Microsoft.Extensions.CommandLineUtils;

var console = new CommandLineApplication
{
    Name = "vitorio-cli",
    Description = "CLI for joking",
    AllowArgumentSeparator = true
};

console.HelpOption("-? | -h | --help");

console.Command("hello", target =>
{
    var name = target.Argument("name", "Say a name");
    target.OnExecute(() =>
    {
        Console.WriteLine($"Hello {name.Values[0]}");
        return 0;
    });
});

console.OnExecute(() =>
{
    console.ShowHelp();
    return 1;
});

console.Execute(args);