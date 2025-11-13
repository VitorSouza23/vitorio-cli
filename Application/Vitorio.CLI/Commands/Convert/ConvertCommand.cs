namespace Vitorio.CLI.Commands.Convert;

public class ConvertCommand : ICommandFactory
{
    public Command Create()
    {
        Command command = new("convert", "Converts data from one type to another");
        command.Subcommands.Add(new ConvertToBae64Command().Create());
        command.Subcommands.Add(new ConvertFromBae64Command().Create());
        return command;
    }
}
