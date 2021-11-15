using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen;

public class GenGuidCommand : ICommandFactory
{
    public Command Create()
    {
        Command command = new("guid", "Gera GUIDs")
        {
            new Option<string>(new string[] { "--format", "-f" }, () => "D", @"Formato de sáida do GUID
Valores possíveis:
 - D -> 00000000-0000-0000-0000-000000000000
 - N -> 00000000000000000000000000000000
 - B -> {00000000-0000-0000-0000-000000000000}
 - P -> (00000000-0000-0000-0000-000000000000)
 - X -> {0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}"),
            new Option<int>(new string[] { "--count", "-c" }, () => Count.Default().Value, "Número de GUIDs a serem gerados")
        };

        command.Handler = CommandHandler.Create((string format, int count, IConsole console) =>
        {
            if (string.IsNullOrWhiteSpace(format) || format is not "D" and not "N" and not "B" and not "P" and not "X")
            {
                console.Error.WriteLine("--format: o valor passado não é válido.");
                return 1;
            }
            else
            {
                if (((Count)count).IsItNotOnRange())
                {
                    console.Error.WriteLine(((Count)count).GetNotInRangeMessage());
                    return 1;
                }

                for (int index = 0; index < count; index++)
                {
                    console.Out.WriteLine(Guid.NewGuid().ToString(format));
                }
            }
            return 0;
        });

        return command;
    }
}
