namespace Vitorio.CLI.Commands.Format;

public class FormatCommand : ICommandFactory
{
    public Command Create()
    {
        Command command = new("format", "Formats data using a mask")
        {
            new FormatCPFCommand().Create(),
            new FormatCNPJCommand().Create(),
            new FormatDateCommand().Create(),
            new FormatStringCommand().Create(),
            new FormatStringListCommand().Create(),
            new FormatGuidCommand().Create()
        };
        return command;
    }
}
