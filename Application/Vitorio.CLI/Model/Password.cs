using System;
using System.Linq;

namespace Vitorio.CLI.Model
{
    public class Password
    {
        private const string CHARS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%&*^";
        private readonly Random _random;

        public Password(Random random)
        {
            _random = random;
        }

        public string New(int length)
        {
            return new string(Enumerable.Repeat(CHARS, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}