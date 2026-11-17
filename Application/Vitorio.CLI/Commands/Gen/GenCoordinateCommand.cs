using System.Globalization;

namespace Vitorio.CLI.Commands.Gen;

public class GenCoordinateCommand : ICommandFactory
{
    public Command Create()
    {
        Option<int> count = new("--count", "-c")
        {
            Description = "Number of coordinate pairs to be generated",
            DefaultValueFactory = _ => Count.Default().Value
        };

        Command command = new("coordinate", "Generate random latitude and longitude coordinates")
        {
            count
        };

        command.SetAction(parseResult =>
        {
            var countValue = parseResult.GetValue(count);

            if (((Count)countValue).IsItNotOnRange())
            {
                Console.Error.WriteLine(((Count)countValue).GetNotInRangeMessage());
                return;
            }

            Random random = new();
            for (int index = 0; index < countValue; index++)
            {
                double latitude = random.NextDouble() * 180 - 90;
                double longitude = random.NextDouble() * 360 - 180;
                Console.WriteLine(
                    $"{latitude.ToString("F6", CultureInfo.InvariantCulture)},{longitude.ToString("F6", CultureInfo.InvariantCulture)}");
            }
        });

        return command;
    }
}
