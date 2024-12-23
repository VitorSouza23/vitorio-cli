using System.Text.Json;

namespace Vitorio.CLI.Commands.Format
{
    public class FormatDateCommand : ICommandFactory
    {
        public Command Create()
        {
            Argument<string> date = new("date", () => "now", "Date value to format [\"now\" to use the current system date, \"utc\" to use the current UTC date]");
            Option<string> mask = new(["--mask", "-m"], () => "dd/MM/yyyy hh:mm:ss", "Mask for date formatting");
            Option<bool> json = new(["--json", "-j"], () => false, "Format the date to JSON");

            Command command = new("date", "Format a date according to a mask")
            {
                date,
                mask,
                json
            };

            command.SetHandler((string date, string mask, bool json, IConsole console) =>
            {
                if (string.IsNullOrWhiteSpace(date))
                {
                    console.Error.WriteLine("Date value cannot be empty");
                    return;
                }

                string dataInput = date switch
                {
                    "now" => DateTime.Now.ToString(),
                    "utc" => DateTime.UtcNow.ToString(),
                    _ => date
                };

                if (DateTime.TryParse(dataInput, out DateTime theDate))
                {
                    if (json)
                    {
                        string jsonDate = JsonSerializer.Serialize(theDate);
                        console.Out.WriteLine(jsonDate);
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(mask))
                        {
                            console.Error.WriteLine("--mask cannot be empty");
                            return;
                        }

                        console.Out.WriteLine(theDate.ToString(mask));
                    }
                }
                else
                {
                    console.Error.WriteLine($"The value {date} was not recognized as a valid date");
                    return;
                }
            }, date, mask, json);

            return command;
        }
    }
}