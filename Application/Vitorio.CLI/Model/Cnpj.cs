using System.Text.RegularExpressions;

namespace Vitorio.CLI.Model;

public class Cnpj
{
    private readonly Random _random;
    public string Value { get; private set; }

    public Cnpj(Random random)
    {
        _random = random;
        Value = string.Empty;
    }

    public static bool IsCnpj(string cnpj)
    {
        return Regex.IsMatch(cnpj, "^[0-9]{14}$");
    }

    public static string RemoveFormat(string cnpj)
    {
        return cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
    }

    public static string Foramt(string cnpj)
    {
        const string mask = @"00\.000\.000\/0000\-00";
        return Convert.ToUInt64(cnpj).ToString(mask);
    }

    public static implicit operator string(Cnpj cnpj) => cnpj.Value;

    public override string ToString()
    {
        return Value;
    }

    public Cnpj New()
    {
        Value = string.Empty;
        int[] multiplier1 = new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplier2 = new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string seed = _random.Next(0, 99999999).ToString("D8");

        seed += "0001";
        seed += CalculateCheckDigit(multiplier1, seed);
        seed += CalculateCheckDigit(multiplier2, seed);

        Value = seed;
        return this;
    }

    private static int CalculateCheckDigit(int[] multiplier, string seed)
    {
        int result = 0;
        for (int index = 0; index < multiplier.Length; index++)
            result += (int)char.GetNumericValue(seed[index]) * multiplier[index];

        int rest = result % 11;
        return rest < 2 ? 0 : 11 - rest;
    }

    public Cnpj Format()
    {
        Value = Foramt(Value);
        return this;
    }
}
