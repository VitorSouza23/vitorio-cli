namespace Vitorio.CLI.Commands.Format;

public class FormatCommand : ICommandFactory
{
    public Command Create()
    {
        Command command = new("format", "Formats data using a mask");
        command.AddCommand(new FormatCPFCommand().Create());
        command.AddCommand(new FormatCNPJCommand().Create());
        command.AddCommand(new FormatDateCommand().Create());
        command.AddCommand(new FormatStringCommand().Create());
        return command;
    }
}
