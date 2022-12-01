namespace Vitorio.CLI.Commands.Format;

public class FormatCommand : ICommandFactory
{
    public Command Create()
    {
        Command command = new("format", "Formata um dado através de uma máscara");
        command.AddCommand(new FormatCPFCommand().Create());
        command.AddCommand(new FormatCNPJCommand().Create());
        command.AddCommand(new FormatDateCommand().Create());
        command.AddCommand(new FormatGitBranchNameCommand().Create());
        return command;
    }
}
