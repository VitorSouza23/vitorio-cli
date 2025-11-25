using System.CommandLine.Help;
using Vitorio.CLI.Commands.About;
using Vitorio.CLI.Commands.Convert;
using Vitorio.CLI.Commands.Date;
using Vitorio.CLI.Commands.Format;
using Vitorio.CLI.Commands.Gen;
using Vitorio.CLI.Settings;

RootCommand rootCommand = new("CLI with some tolls to make your developer life easier (or not)")
{
    new AboutCommand().Create(),
    new GenCommand().Create(),
    new FormatCommand().Create(),
    new ConvertCommand().Create(),
    new DateCommand().Create()
};

var defaultHelpOption = rootCommand.Options.FirstOrDefault(o => o is HelpOption) as HelpOption;
defaultHelpOption!.Action = new CustomHelpAction((HelpAction)defaultHelpOption.Action!);


ParseResult parseResult = rootCommand.Parse(args);
return parseResult.Invoke();