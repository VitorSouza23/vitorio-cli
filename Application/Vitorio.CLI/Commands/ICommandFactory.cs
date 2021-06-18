using System.CommandLine;

namespace Vitorio.CLI.Commands
{
    public interface ICommandFactory
    {
        Command Create();
    }
}