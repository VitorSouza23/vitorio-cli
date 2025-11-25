using System.CommandLine.Binding;
using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands;

public class GenPhoneCommand : ICommandFactory
{
    public Command Create()
    {
        Option<int> codeCountry = new("--code-country", "-cc")
        {
            Description = "Country code",
            DefaultValueFactory = _ => 0
        };
        Option<int> ddd = new("--ddd", "-d")
        {
            Description = "DDD code of the destination location",
            DefaultValueFactory = _ => 0
        };
        Option<int> numberOfDigits = new("--number-of-digits", "-nd")
        {
            Description = "Number of digits in the number (Min = 3, Max = 9)",
            DefaultValueFactory = _ => 9
        };
        Option<int> count = new("--count", "-c")
        {
            Description = "Number of telephone numbers to be generated",
            DefaultValueFactory = _ => Count.Default().Value
        };
        Option<bool> notFormatted = new("--not-formatted", "-nf")
        {
            Description = "Do not format the phone number",
            DefaultValueFactory = _ => false
        };

        Command command = new("phone", "Generate phone number")
        {
            codeCountry,
            ddd,
            numberOfDigits,
            count,
            notFormatted
        };

        command.SetAction(parseResult =>
        {
            var codeCountryValue = parseResult.GetValue(codeCountry);
            var dddValue = parseResult.GetValue(ddd);
            var numberOfDigitsValue = parseResult.GetValue(numberOfDigits);
            var countValue = parseResult.GetValue(count);
            var notFormattedValue = parseResult.GetValue(notFormatted);

            if (((Count)countValue).IsItNotOnRange())
            {
                Console.Error.WriteLine(((Count)countValue).GetNotInRangeMessage());
                return;
            }

            PhoneRules phoneRules = new(codeCountryValue, dddValue, numberOfDigitsValue, notFormattedValue);
            Phone phone = new(new Random(), phoneRules);

            if (phone.IsNotNumberOfDigitsInRange())
            {
                Console.Error.WriteLine(Phone.GetNotInRangeMessage());
                return;
            }

            for (int index = 0; index < countValue; index++)
            {
                Console.WriteLine(phone.New());
            }
        });

        return command;
    }
}
