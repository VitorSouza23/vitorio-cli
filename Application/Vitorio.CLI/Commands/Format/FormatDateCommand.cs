using System.Text.Json;

namespace Vitorio.CLI.Commands.Format
{
    public class FormatDateCommand : ICommandFactory
    {
        public Command Create()
        {
            Argument<string> date = new("date")
            {
                Description = "Date value to format [\"now\" to use the current system date, \"utc\" to use the current UTC date]",
                DefaultValueFactory = _ => "now"
            };

            Option<string> mask = new("--mask", "-m")
            {
                Description = "Mask for date formatting",
                DefaultValueFactory = _ => "dd/MM/yyyy hh:mm:ss"
            };
            
            Option<bool> json = new("--json", "-j")
            {
                Description = "Format the date to JSON",
                DefaultValueFactory = _ => false
            };

            Command command = new("date", "Format a date according to a mask")
            {
                date,
                mask,
                json
            };

            command.SetAction(parseResult =>
            {
                var dateValue = parseResult.GetValue(date);
                var maskValue = parseResult.GetValue(mask);
                var jsonValue = parseResult.GetValue(json);

                if (string.IsNullOrWhiteSpace(dateValue))
                {
                    Console.Error.WriteLine("Date value cannot be empty");
                    return;
                }

                string dataInput = dateValue switch
                {
                    "now" => DateTime.Now.ToString(),
                    "utc" => DateTime.UtcNow.ToString(),
                    _ => dateValue
                };

                if (DateTime.TryParse(dataInput, out DateTime theDate))
                {
                    if (jsonValue)
                    {
                        string jsonDate = JsonSerializer.Serialize(theDate);
                        Console.WriteLine(jsonDate);
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(maskValue))
                        {
                            Console.Error.WriteLine("--mask cannot be empty");
                            return;
                        }

                        Console.WriteLine(theDate.ToString(maskValue));
                    }
                }
                else
                {
                    Console.Error.WriteLine($"The value {date} was not recognized as a valid date");
                    return;
                }
            });

            return command;
        }
    }
}