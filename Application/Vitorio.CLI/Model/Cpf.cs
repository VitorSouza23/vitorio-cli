using System;
using System.Text.RegularExpressions;

namespace Vitorio.CLI.Model
{
    public class Cpf
    {
        private readonly Random _random;

        public Cpf(Random random)
        {
            _random = random;
            Value = string.Empty;
        }

        public string Value { get; private set; }

        public static implicit operator string(Cpf cpf) => cpf.Value;

        public static string Format(string cpf)
        {
            const string cpfMask = @"000\.000\.000\-00";
            return Convert.ToUInt64(cpf).ToString(cpfMask);
        }

        public static string RemoveFormat(string cpf)
        {
            return cpf.Replace(".", "").Replace("-", "");
        }

        public static bool IsCPF(string cpf)
        {
            return Regex.IsMatch(cpf, "^[0-9]{11}$");
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

        private int CalculateCheckDigit(int[] multiplier, string seed)
        {
            int remainder = 0, sum = 0;

            for (int index = 0; index < multiplier.Length; index++)
                sum += int.Parse(seed[index].ToString()) * multiplier[index];

            remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            return remainder;
        }

        public Cpf Format()
        {
            Value = Format(Value);
            return this;
        }
    }
}