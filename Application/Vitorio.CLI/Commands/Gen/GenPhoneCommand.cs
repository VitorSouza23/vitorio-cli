using System.CommandLine.Binding;
using Vitorio.CLI.Model;

namespace Vitorio.CLI.Commands;

public class GenPhoneCommand : ICommandFactory
{
    public Command Create()
    {
        Option<int> codeCountry = new(new string[] { "--code-country", "-cc" }, () => 0, "Código do país");
        Option<int> ddd = new(new string[] { "--ddd", "-d" }, () => 0, "Código DDD da localidade de destino");
        Option<int> numberOfDigits = new(new string[] { "--number-of-digits", "-nd" }, () => 9, "Quantidade de dígitos no número (Min = 3, Max = 9)");
        Option<int> count = new(new string[] { "--count", "-c" }, () => Count.Default().Value, "Quantidade de números telefônicos a serem gerados");
        Option<bool> notFormated = new(new string[] { "--not-formated", "-nf" }, () => false, "Não formata o número telefônico");

        Command command = new("phone", "Gera número de telefone")
        {
            codeCountry,
            ddd,
            numberOfDigits,
            count,
            notFormated
        };

        CustomPhoneBinder customPhoneBinder = new(codeCountry, ddd, numberOfDigits, notFormated);

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
                console.Error.WriteLine(phone.GetNotInRangeMessage());
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

internal class CustomPhoneBinder : BinderBase<PhoneRules>
{
    private readonly Option<int> _codeCountry;
    private readonly Option<int> _ddd;
    private readonly Option<int> _numberOfDigits;
    private readonly Option<bool> _notFormated;

    public CustomPhoneBinder(Option<int> codeCountry, Option<int> ddd, Option<int> numberOfDigits, Option<bool> notFormated)
    {
        _codeCountry = codeCountry;
        _ddd = ddd;
        _numberOfDigits = numberOfDigits;
        _notFormated = notFormated;
    }

    protected override PhoneRules GetBoundValue(BindingContext bindingContext)
    {
        return new PhoneRules(bindingContext.ParseResult.GetValueForOption(_codeCountry),
                              bindingContext.ParseResult.GetValueForOption(_ddd),
                              bindingContext.ParseResult.GetValueForOption(_numberOfDigits),
                              bindingContext.ParseResult.GetValueForOption(_notFormated));
    }
}
