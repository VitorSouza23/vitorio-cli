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
            return command;
        }
    }
}