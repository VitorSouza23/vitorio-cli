using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands.Gen;

public class GenGuidCommand : ICommandFactory
{
    public Command Create()
    {
        Option<string> format = new("--format", "-f")
        {
            Description = """
            GUID Output Format
            Possible values:
            - D -> 00000000-0000-0000-0000-00000000000
            - N -> 000000000000000000000000000000
            - B -> {00000000-0000-0000-0000-00000000000}
            - P -> (00000000-0000-0000-0000-0000000000)
            - X -> {0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}
            """,
            DefaultValueFactory = _ => "D"
        };
        Option<int> count = new("--count", "-c")
        {
            Description = "Number of GUIDs to generate",
            DefaultValueFactory = _ => Count.Default().Value
        };
        Option<int> version = new("--version", "-v")
        {
            Description = "UUID version to generate (4 or 7)",
            DefaultValueFactory = _ => 4
        };

        Command command = new("guid", "Generate GUIDs")
        {
            format,
            count,
            version
        };

        command.SetAction(parseResult =>
        {
            var formatValue = parseResult.GetValue(format);
            var countValue = parseResult.GetValue(count);
            var versionValue = parseResult.GetValue(version);

            if (string.IsNullOrWhiteSpace(formatValue) || formatValue is not "D" and not "N" and not "B" and not "P" and not "X")
            {
                Console.Error.WriteLine("--format: The value passed is not valid.");
                return;
            }
            else if (versionValue is not 4 and not 7)
            {
                Console.Error.WriteLine("--version: The value passed must be 4 or 7.");
                return;
            }
            else
            {
                if (((Count)countValue).IsItNotOnRange())
                {
                    Console.Error.WriteLine(((Count)countValue).GetNotInRangeMessage());
                    return;
                }

                for (int index = 0; index < countValue; index++)
                {
                    Guid guid = versionValue == 7 ? Guid.CreateVersion7() : Guid.NewGuid();
                    Console.WriteLine(guid.ToString(formatValue));
                }
            }
        });

        return command;
    }
}
