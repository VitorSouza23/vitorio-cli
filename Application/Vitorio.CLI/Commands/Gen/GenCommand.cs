namespace Vitorio.CLI.Commands.Gen;

public class GenCommand : ICommandFactory
{
    public Command Create()
    {
        Command command = new("gen", "Generates some kind of data")
        {
            new GenCPFCommand().Create(),
            new GenCNPJCommand().Create(),
            new GenGuidCommand().Create(),
            new GenEmailCommand().Create(),
            new GenPhoneCommand().Create(),
            new GenPasswordCommand().Create(),
            new GenNameCommand().Create(),
            new GenCEPCommand().Create(),
            new GenBirthDateCommand().Create()
        };
        
        return command;
    }
}
