namespace Vitorio.CLI.Commands.Convert;

public class ConvertCommand : ICommandFactory
{
    public Command Create()
    {
        Command command = new("convert", "Converte um dado de um tipo para outro");
        command.AddCommand(new ConvertToBae64Command().Create());
        return command;
    }
}
