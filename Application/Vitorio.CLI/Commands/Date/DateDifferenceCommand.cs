namespace Vitorio.CLI.Commands.Date;

public class DateDifferenceCommand : ICommandFactory
{
    public Command Create()
    {
        Argument<string> start = new("start"){
            Description = "The start date [\"now\" to use the current system date, \"utc\" to use the current UTC date]"
        };
        Argument<string> end = new("end"){
            Description = "The end date [\"now\" to use the current system date, \"utc\" to use the current UTC date]"
        };

        Command command = new("difference", "Calculate the difference between two dates")
        {
            start,
            end
        };

        command.SetAction(parseResult =>
        {
            var startValue = parseResult.GetValue(start);
            var endValue = parseResult.GetValue(end);
            if (string.IsNullOrWhiteSpace(startValue))
            {
                Console.Error.WriteLine("Start date cannot be empty");
                return;
            }

            if (string.IsNullOrWhiteSpace(endValue))
            {
                Console.Error.WriteLine("End date cannot be empty");
                return;
            }

            string startInput = startValue switch
            {
                "now" => DateTime.Now.ToString(),
                "utc" => DateTime.UtcNow.ToString(),
                _ => startValue
            };

            string endInput = endValue switch
            {
                "now" => DateTime.Now.ToString(),
                "utc" => DateTime.UtcNow.ToString(),
                _ => endValue
            };

            if (DateTime.TryParse(startInput, out DateTime startDate) && DateTime.TryParse(endInput, out DateTime endDate))
            {
                TimeSpan difference = endDate - startDate;
                Console.WriteLine(difference.ToString());
            }
            else
            {
                Console.Error.WriteLine("Invalid date format");
                return;
            }
        });

        return command;
    }
}