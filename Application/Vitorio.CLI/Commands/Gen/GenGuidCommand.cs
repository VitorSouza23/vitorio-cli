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

        Command command = new("guid", "Generate GUIDs")
        {
            format,
            count
        };

        command.SetAction(parseResult =>
        {
            var formatValue = parseResult.GetValue(format);
            var countValue = parseResult.GetValue(count);

            if (string.IsNullOrWhiteSpace(formatValue) || formatValue is not "D" and not "N" and not "B" and not "P" and not "X")
            {
                Console.Error.WriteLine("--format: The value passed is not valid.");
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
                    Console.WriteLine(Guid.NewGuid().ToString(formatValue));
                }
            }
        });

        return command;
    }
}
