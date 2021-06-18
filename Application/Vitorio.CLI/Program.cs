using System.CommandLine;
using Vitorio.CLI.Commands.Gen;

RootCommand rootCommand = new("CLI to help developers in day to day with small tools");
rootCommand.AddCommand(new GenCommand().Create());

return rootCommand.InvokeAsync(args).Result;