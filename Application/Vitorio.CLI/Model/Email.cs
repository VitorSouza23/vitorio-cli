using System.Linq;

namespace Vitorio.CLI.Model;

public class Email
{
    private const int RANDOM_STRING_LENGTH = 5;
    private const string DEAFULT_DOMAIN = "com";

    private readonly Random _random;
    private readonly string _provider;
    private readonly string _domain;

    public Email(Random random, string provider = null, string domain = null)
    {
        _random = random;
        _provider = string.IsNullOrWhiteSpace(provider) ?
            RandomString(random, RANDOM_STRING_LENGTH) :
            provider;
        _domain = string.IsNullOrWhiteSpace(domain) ?
            DEAFULT_DOMAIN :
            domain;
    }

    public string Value => $"{RandomString(_random, RANDOM_STRING_LENGTH)}@{_provider}.{_domain}";

    public static implicit operator string(Email email) => email.Value;

    public override string ToString()
    {
        return Value;
    }

    private static string RandomString(Random random, int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
