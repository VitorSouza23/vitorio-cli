using System.CommandLine.Help;
using System.CommandLine.Invocation;
using Spectre.Console;

namespace Vitorio.CLI.Settings;

internal class CustomHelpAction(HelpAction action) : SynchronousCommandLineAction
{
    public override int Invoke(ParseResult parseResult)
    {
        AnsiConsole.Write(new FigletText("Vitorio.CLI"));
        return action.Invoke(parseResult);
    }
}
