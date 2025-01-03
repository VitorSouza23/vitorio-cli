namespace Vitorio.CLI.Commands.Gen;

public class GenCommand : ICommandFactory
{
    public Command Create()
    {
        Command command = new("gen", "Generates some kind of data");
        command.AddCommand(new GenCPFCommand().Create());
        command.AddCommand(new GenCNPJCommand().Create());
        command.AddCommand(new GenGuidCommand().Create());
        command.AddCommand(new GenEmailCommand().Create());
        command.AddCommand(new GenPhoneCommand().Create());
        command.AddCommand(new GenPasswordCommand().Create());
        command.AddCommand(new GenNameCommand().Create());
        command.AddCommand(new GenCEPCommand().Create());
        command.AddCommand(new GenBirthDateCommand().Create());
        return command;
    }
}
