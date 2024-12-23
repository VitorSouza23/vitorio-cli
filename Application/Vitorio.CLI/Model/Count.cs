namespace Vitorio.CLI.Model;

public class Count(int value)
{
    public const int DefaultMaxValue = 1000;
    public const int DefaultMinValue = 1;

    public int MaxValue { get; set; } = DefaultMaxValue;
    public int MinValue { get; set; } = DefaultMinValue;

    public int Value { get; private set; } = value;

    public Count(string value) : this(int.Parse(value))
    {

    }

    public static Count Default() => new(1);

    public static implicit operator int(Count count) => count.Value;
    public static implicit operator Count(int count) => new(count);

    public bool IsItOnRange() => Value >= MinValue && Value <= MaxValue;
    public bool IsItNotOnRange() => !IsItOnRange();
    public string GetNotInRangeMessage() => $"--count deve estar entre {MinValue} e {MaxValue}";
}
