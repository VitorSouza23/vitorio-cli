using System.CommandLine.Builder;
using System.CommandLine.Help;
using System.CommandLine.Parsing;
using System.Linq;
using Spectre.Console;
using Vitorio.CLI.Commands.About;
using Vitorio.CLI.Commands.Convert;
using Vitorio.CLI.Commands.Format;
using Vitorio.CLI.Commands.Gen;

RootCommand rootCommand = new("CLI com ferramentas que podem (ou não) serem úteis para o dia a dia do desenvolvedor");
rootCommand.AddCommand(new AboutCommand().Create());
rootCommand.AddCommand(new GenCommand().Create());
rootCommand.AddCommand(new FormatCommand().Create());
rootCommand.AddCommand(new ConvertCommand().Create());

var parser = new CommandLineBuilder(rootCommand)
    .UseDefaults()
    .UseHelp(ctx =>
    {
        ctx.HelpBuilder.CustomizeLayout(_ =>
            HelpBuilder.Default
                .GetLayout()
                .Skip(1)
                .Prepend(_ => Spectre.Console.AnsiConsole.Write(new FigletText("Vitorio.CLI"))));
    })
    .Build();

return await parser.InvokeAsync(args);