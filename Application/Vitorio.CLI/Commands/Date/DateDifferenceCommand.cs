namespace Vitorio.CLI.Commands.Date;

public class DateDifferenceCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> start = new("start", "Start date [\"now\" to use the current system date, \"utc\" to use the current UTC date]");
        Argument<string> end = new("end", "End date [\"now\" to use the current system date, \"utc\" to use the current UTC date]");

        Command command = new("difference", "Calculate the difference between two dates")
        {
            start,
            end
        };

        command.SetHandler((string start, string end, IConsole console) =>
        {
            if (string.IsNullOrWhiteSpace(start))
            {
                console.Error.WriteLine("Start date cannot be empty");
                return;
            }

            if (string.IsNullOrWhiteSpace(end))
            {
                console.Error.WriteLine("End date cannot be empty");
                return;
            }

            string startInput = start switch
            {
                "now" => DateTime.Now.ToString(),
                "utc" => DateTime.UtcNow.ToString(),
                _ => start
            };

            string endInput = end switch
            {
                "now" => DateTime.Now.ToString(),
                "utc" => DateTime.UtcNow.ToString(),
                _ => end
            };

            if (DateTime.TryParse(startInput, out DateTime startDate) && DateTime.TryParse(endInput, out DateTime endDate))
            {
                TimeSpan difference = endDate - startDate;
                console.Out.WriteLine(difference.ToString());
            }
            else
            {
                console.Error.WriteLine("Invalid date format");
                return;
            }
        }, start, end);

        return command;
    }
}