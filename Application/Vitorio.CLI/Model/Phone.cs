using System.Text;

namespace Vitorio.CLI.Model;

public record PhoneRules(int CodeCountry, int Ddd, int NumberOfDigits, bool NotFormatted);
public class Phone(Random random, PhoneRules rules)
{
    public const int MAX_LENGTH = 9;
    public const int MIN_LENGTH = 3;

    private readonly Random _random = random;
    private readonly PhoneRules _rules = rules;

    public bool IsNumberOfDigitsInRange() => _rules.NumberOfDigits is >= MIN_LENGTH and <= MAX_LENGTH;
    public bool IsNotNumberOfDigitsInRange() => !IsNumberOfDigitsInRange();
    public static string GetNotInRangeMessage() => "--number-of-digits must be between 3 and 10";

    public string New()
    {
        StringBuilder stringBuilder = new();
        for (int index = 0; index < _rules.NumberOfDigits; index++)
        {
            stringBuilder.Append('9');
        }

        int maxNumberValue = int.Parse(stringBuilder.ToString());
        string phoneNumber = _random.Next(0, maxNumberValue).ToString($"D{_rules.NumberOfDigits}");

        stringBuilder = new();

        if (_rules.CodeCountry > 0)
        {
            stringBuilder.Append(_rules.NotFormatted ? _rules.CodeCountry : $"+{_rules.CodeCountry}");
            stringBuilder.Append(' ');
        }

        if (_rules.Ddd > 0)
        {
            stringBuilder.Append(_rules.NotFormatted ? _rules.Ddd : $"({_rules.Ddd})");
            stringBuilder.Append(' ');
        }

        if (_rules.NotFormatted is false && _rules.NumberOfDigits >= 7)
            stringBuilder.Append(phoneNumber.Insert(phoneNumber.Length - 4, "-"));
        else
            stringBuilder.Append(phoneNumber);

        return stringBuilder.ToString();
    }
}
