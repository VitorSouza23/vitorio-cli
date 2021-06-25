using System.CommandLine;

namespace Vitorio.CLI.Commands.Gen
{
    public class GenCommand : ICommandFactory
    {
        public Command Create()
        {
            Command command = new("gen", "Gera algum tipo de dado");
            command.AddCommand(new GenCPFCommand().Create());
            command.AddCommand(new GenCNPJCommand().Create());
            command.AddCommand(new GenGuidCommand().Create());
            command.AddCommand(new GenEmailCommand().Create());
            command.AddCommand(new GenPhoneCommand().Create());
            command.AddCommand(new GenPasswordCommand().Create());
            return command;
        }
    }
}