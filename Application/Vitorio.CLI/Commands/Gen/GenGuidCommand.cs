using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen;

public class GenGuidCommand : ICommandFactory
{
    public Command Create()
    {
        Option<string> format = new(["--format", "-f"], () => "D",
    """
    GUID Output Format
    Possible values:
    - D -> 00000000-0000-0000-0000-00000000000
    - N -> 000000000000000000000000000000
    - B -> {00000000-0000-0000-0000-00000000000}
    - P -> (00000000-0000-0000-0000-0000000000)
    - X -> {0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}
    """);
        Option<int> count = new(["--count", "-c"], () => Count.Default().Value, "Number of GUIDs to generate");

        Command command = new("guid", "Generate GUIDs")
        {
            format,
            count
        };

        command.SetHandler((string format, int count, IConsole console) =>
        {
            if (string.IsNullOrWhiteSpace(format) || format is not "D" and not "N" and not "B" and not "P" and not "X")
            {
                console.Error.WriteLine("--format: The value passed is not valid.");
                return;
            }
            else
            {
                if (((Count)count).IsItNotOnRange())
                {
                    console.Error.WriteLine(((Count)count).GetNotInRangeMessage());
                    return;
                }

                for (int index = 0; index < count; index++)
                {
                    console.Out.WriteLine(Guid.NewGuid().ToString(format));
                }
            }
        }, format, count);

        return command;
    }
}
