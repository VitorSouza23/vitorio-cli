using System;
using System.Text;

namespace Vitorio.CLI.Model
{
    public record PhoneRules(int CodeCountry, int Ddd, int NumberOfDigits, bool NotFormated);
    public class Phone
    {
        private readonly Random _random;

        public Phone(Random random)
        {
            _random = random;
        }

        public string New(PhoneRules phoneRules)
        {
            StringBuilder stringBuilder = new();
            for (int index = 0; index < phoneRules.NumberOfDigits; index++)
            {
                stringBuilder.Append("9");
            }

            int maxNumberValue = int.Parse(stringBuilder.ToString());
            string phoneNumber = _random.Next(0, maxNumberValue).ToString($"D{phoneRules.NumberOfDigits}");

            stringBuilder = new();

            if (phoneRules.CodeCountry > 0)
            {
                stringBuilder.Append(phoneRules.NotFormated ? phoneRules.CodeCountry : $"+{phoneRules.CodeCountry}");
                stringBuilder.Append(" ");
            }

            if (phoneRules.Ddd > 0)
            {
                stringBuilder.Append(phoneRules.NotFormated ? phoneRules.Ddd : $"({phoneRules.Ddd})");
                stringBuilder.Append(" ");
            }

            if (phoneRules.NotFormated is false && phoneRules.NumberOfDigits >= 7)
                stringBuilder.Append(phoneNumber.Insert(phoneNumber.Length - 4, "-"));
            else
                stringBuilder.Append(phoneNumber);

            return stringBuilder.ToString();
        }

    }
}