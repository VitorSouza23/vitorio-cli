namespace Vitorio.CLI.Commands.Date;

public class DateAddCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> start = new("start", "Start date [\"now\" to use the current system date, \"utc\" to use the current UTC date]");
        Argument<string> time = new("time", "Time to be added");

        Command command = new("add", "Add a time to a date")
        {
            start,
            time
        };

        command.SetHandler((string start, string time, IConsole console) =>
        {
            if (string.IsNullOrWhiteSpace(start))
            {
                console.Error.WriteLine("Start date cannot be empty");
                return;
            }

            if (string.IsNullOrWhiteSpace(time))
            {
                console.Error.WriteLine("Time to be added cannot be empty");
                return;
            }

            string startInput = start switch
            {
                "now" => DateTime.Now.ToString(),
                "utc" => DateTime.UtcNow.ToString(),
                _ => start
            };

            if (DateTime.TryParse(startInput, out DateTime startDate) && TimeSpan.TryParse(time, out TimeSpan timeToAdd))
            {
                DateTime result = startDate.Add(timeToAdd);
                console.Out.WriteLine(result.ToString());
            }
            else
            {
                console.Error.WriteLine("Invalid date or time format");
                return;
            }
        }, start, time);

        return command;
    }
}
