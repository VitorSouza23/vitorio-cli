using System;
using System.Text.RegularExpressions;

namespace Vitorio.CLI.Extensions
{
    public static class CNPJExtensions
    {

        public static string FormatCNPJ(this string cnpj)
        {
            const string mask = @"00\.000\.000\/0000\-00";
            return Convert.ToUInt64(cnpj).ToString(mask);
        }

        public static string RemoveCNPJFormatation(this string cnpj)
        {
            return cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
        }

        public static bool IsCNPJ(this string cnpj)
        {
            return Regex.IsMatch(cnpj, "^[0-9]{14}$");
        }
    }
}