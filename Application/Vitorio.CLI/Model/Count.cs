namespace Vitorio.CLI.Model;

public class Count
{
    public const int DefaultMaxValue = 1000;
    public const int DefaultMinValue = 1;

    public int MaxValue { get; set; }
    public int MinValue { get; set; }

    public int Value { get; private set; }

    public Count(int value)
    {
        Value = value;
        MaxValue = DefaultMaxValue;
        MinValue = DefaultMinValue;
    }

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
