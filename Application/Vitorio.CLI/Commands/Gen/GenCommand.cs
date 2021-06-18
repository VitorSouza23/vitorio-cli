using System.CommandLine;

namespace Vitorio.CLI.Commands.Gen
{
    public class GenCommand : ICommandFactory
    {
        public Command Create()
        {
            Command command = new("gen", "Generete some data");
            command.AddCommand(new GenCPFCommand().Create());
            return command;
        }
    }
}