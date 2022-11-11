namespace Vitorio.CLI.Model;

public class CEP
{
    private readonly Random _random;
    public string Value { get; private set; }

    public CEP(Random random)
    {
        _random = random;
        Value = string.Empty;
    }

    public static implicit operator string(CEP cep) => cep.Value;

    public override string ToString()
    {
        return Value;
    }

    public CEP New()
    {
        Value = _random.Next(0, 99999999).ToString("D8");
        return this;
    }

    public CEP Format()
    {
        if (Value.Length > 5) Value = Value.Insert(5, "-");
        return this;
    }
}
