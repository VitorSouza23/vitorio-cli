
namespace Vitorio.CLI.Commands.Date;

public class DateCommand : ICommandFactory
{
    public Command Create()
    {
        Command command = new("date", "Date operations")
        {
            new DateDifferenceCommand().Create(),
            new DateAddCommand().Create()
        };
        return command;
    }
}
