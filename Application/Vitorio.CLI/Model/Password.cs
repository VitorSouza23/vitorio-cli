namespace Vitorio.CLI.Model;

public class Password
{
    public const int MAX_LENGTH = 50;
    public const int MIN_LENGTH = 3;

    private const string CHARS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%&*^";
    private readonly Random _random;
    private readonly int _length;

    public Password(Random random, int length = 8)
    {
        _random = random;
        _length = length;
    }

    public bool IsLengthInRange() => _length is >= MIN_LENGTH and <= MAX_LENGTH;

    public static string GetLengthOutOfRangeMessage() => $"--length must be greater than {MIN_LENGTH} and less than {MAX_LENGTH}";

    public string New()
    {
        if (IsLengthInRange())
            return new string(Enumerable.Repeat(CHARS, _length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        else
            return string.Empty;
    }
}
