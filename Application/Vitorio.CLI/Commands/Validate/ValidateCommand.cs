namespace Vitorio.CLI.Commands.Validate;

public class ValidateCommand : ICommandFactory
{
    public Command Create()
    {
        return new Command("validate", "Validates data")
        {
            new ValidateJsonCommand().Create()
        };
    }
}
