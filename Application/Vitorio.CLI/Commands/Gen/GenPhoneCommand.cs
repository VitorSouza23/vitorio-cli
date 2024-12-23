using System.CommandLine.Binding;
using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands;

public class GenPhoneCommand : ICommandFactory
{
    public Command Create()
    {
        Option<int> codeCountry = new(["--code-country", "-cc"], () => 0, "Country code");
        Option<int> ddd = new(["--ddd", "-d"], () => 0, "DDD code of the destination location");
        Option<int> numberOfDigits = new(["--number-of-digits", "-nd"], () => 9, "Number of digits in the number (Min = 3, Max = 9)");
        Option<int> count = new(["--count", "-c"], () => Count.Default().Value, "Number of telephone numbers to be generated");
        Option<bool> notFormatted = new(["--not-formatted", "-nf"], () => false, "Does not format the phone number");

        Command command = new("phone", "Generate phone number")
        {
            codeCountry,
            ddd,
            numberOfDigits,
            count,
            notFormatted
        };

        CustomPhoneBinder customPhoneBinder = new(codeCountry, ddd, numberOfDigits, notFormatted);

        command.SetHandler((PhoneRules phoneRules, int count, IConsole console) =>
        {
            if (((Count)count).IsItNotOnRange())
            {
                console.Error.WriteLine(((Count)count).GetNotInRangeMessage());
                return;
            }

            Phone phone = new(new Random(), phoneRules);

            if (phone.IsNotNumberOfDigitsInRange())
            {
                console.Error.WriteLine(Phone.GetNotInRangeMessage());
                return;
            }

            for (int index = 0; index < count; index++)
            {
                console.Out.WriteLine(phone.New());
            }
        }, customPhoneBinder, count);

        return command;
    }
}

internal class CustomPhoneBinder(Option<int> codeCountry, Option<int> ddd, Option<int> numberOfDigits, Option<bool> notFormatted) : BinderBase<PhoneRules>
{
    private readonly Option<int> _codeCountry = codeCountry;
    private readonly Option<int> _ddd = ddd;
    private readonly Option<int> _numberOfDigits = numberOfDigits;
    private readonly Option<bool> _notFormatted = notFormatted;

    protected override PhoneRules GetBoundValue(BindingContext bindingContext)
    {
        return new PhoneRules(bindingContext.ParseResult.GetValueForOption(_codeCountry),
                              bindingContext.ParseResult.GetValueForOption(_ddd),
                              bindingContext.ParseResult.GetValueForOption(_numberOfDigits),
                              bindingContext.ParseResult.GetValueForOption(_notFormatted));
    }
}
