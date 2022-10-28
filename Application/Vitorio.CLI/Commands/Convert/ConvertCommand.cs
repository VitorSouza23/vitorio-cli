namespace Vitorio.CLI.Commands.Convert;

public class ConvertCommand : ICommandFactory
{
    public Command Create()
    {
        Command command = new("convert", "Converte um dado de um tipo para outro");
        command.AddCommand(new ConvertImageToBae64Command().Create());
        return command;
    }
}
