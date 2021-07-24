using System;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace Vitorio.CLI.Model
{
    public enum NameGender
    {
        All = 0,
        Feminine = 1,
        Masculine = 2,
        NotDefined = 3
    }

    public class Name
    {
        private readonly Random _random;
        private readonly string[] _femaleNames;
        private readonly string[] _maleNames;
        private readonly string[] _lastNames;

        public record NameCollection(string[] Names);

        public Name(Random random)
        {
            _random = random;
            _femaleNames = GetNamesFromJsonFile("names-f");
            _maleNames = GetNamesFromJsonFile("names-m");
            _lastNames = GetNamesFromJsonFile("lastnames"); ;
        }

        private string[] GetNamesFromJsonFile(string fileName)
        {
            string jsonContent = File.ReadAllText($"Resources/{fileName}.json");
            var namesCollection = JsonSerializer.Deserialize<NameCollection>(jsonContent);
            return namesCollection.Names;
        }
        public string New(NameGender gender = NameGender.All)
        {
            string[] names = gender switch
            {
                NameGender.All => _femaleNames.Concat(_maleNames).ToArray(),
                NameGender.Feminine => _femaleNames,
                NameGender.Masculine => _maleNames,
                _ => new string[0]
            };

            string firstName = names[_random.Next(names.Length)];
            string lastname = _lastNames[_random.Next(_lastNames.Length)];

            return $"{firstName} {lastname}";
        }
    }
}