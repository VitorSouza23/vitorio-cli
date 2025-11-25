namespace Vitorio.CLI.Commands.Date;

public class DateAddCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> start = new("start")
        {
            Description = "The start date [\"now\" to use the current system date, \"utc\" to use the current UTC date]"
        };
        Argument<string> time = new("time")
        {
            Description = "The time to be added"
        };

        Command command = new("add", "Add a time to a date")
        {
            start,
            time
        };

        command.SetAction(parseResult =>
        {
            var startValue = parseResult.GetValue(start);
            var timeValue = parseResult.GetValue(time);
            if (string.IsNullOrWhiteSpace(startValue))
            {
                Console.Error.WriteLine("Start date cannot be empty");
                return;
            }

            if (string.IsNullOrWhiteSpace(timeValue))
            {
                Console.Error.WriteLine("Time to be added cannot be empty");
                return;
            }

            string startInput = startValue switch
            {
                "now" => DateTime.Now.ToString(),
                "utc" => DateTime.UtcNow.ToString(),
                _ => startValue
            };

            if (DateTime.TryParse(startInput, out DateTime startDate) && TimeSpan.TryParse(timeValue, out TimeSpan timeToAdd))
            {
                DateTime result = startDate.Add(timeToAdd);
                Console.Out.WriteLine(result.ToString());
            }
            else
            {
                Console.Error.WriteLine("Invalid date or time format");
            }
        });

        return command;
    }
}
