
namespace Vitorio.CLI.Commands.Date;

public class DateCommand : ICommandFactory
{
    public Command Create()
    {
        Command command = new("date", "Date operations");
        command.AddCommand(new DateDifferenceCommand().Create());
        command.AddCommand(new DateAddCommand().Create());
        return command;
    }
}
