using System;
using System.Text.RegularExpressions;

namespace Vitorio.CLI.Extensions
{
    public static class CPFExtensions
    {

        public static string FormatCPF(this string cpf)
        {
            const string cpfMask = @"000\.000\.000\-00";
            return Convert.ToUInt64(cpf).ToString(cpfMask);
        }

        public static string RemoveCPFFormatation(this string cpf)
        {
            return cpf.Replace(".", "").Replace("-", "");
        }

        public static bool IsCPF(this string cpf)
        {
            return Regex.IsMatch(cpf, "^[0-9]{11}$");
        }
    }
}