using System.Text.RegularExpressions;

namespace Vitorio.CLI.Model;

public class Cpf
{
    private readonly Random _random;

    public Cpf(Random random, bool putInitializeValue = true)
    {
        _random = random;
        Value = putInitializeValue ? New() : string.Empty;
    }

    public string Value { get; private set; }

    public static implicit operator string(Cpf cpf) => cpf.Value;

    public static string Format(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return string.Empty;

        const string cpfMask = @"000\.000\.000\-00";
        return Convert.ToUInt64(cpf).ToString(cpfMask);
    }

    public static string RemoveFormat(string cpf)
    {
        return cpf.Replace(".", "").Replace("-", "");
    }

    public static bool IsCPF(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        var clenaedCpf = RemoveFormat(cpf);
        return Regex.IsMatch(clenaedCpf, "^[0-9]{11}$");
    }

    public override string ToString()
    {
        return Value;
    }

    public Cpf New()
    {
        int[] multiplier1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplier2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string seed = _random.Next(0, 999999999).ToString("D9");

        seed += CalculateCheckDigit(multiplier1, seed);
        seed += CalculateCheckDigit(multiplier2, seed);
        Value = seed;

        return this;
    }

    private static int CalculateCheckDigit(int[] multiplier, string seed)
    {
        int sum = 0;

        for (int index = 0; index < multiplier.Length; index++)
            sum += int.Parse(seed[index].ToString()) * multiplier[index];

        int remainder = sum % 11;

        return remainder < 2 ? 0 : 11 - remainder;
    }

    public Cpf Format()
    {
        Value = Format(Value);
        return this;
    }
}
