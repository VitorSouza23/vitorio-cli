namespace Vitorio.CLI.Model;

public sealed class BirthDate
{
    private readonly DateTime? _referenceDateTime;

    public BirthDate() { }

    public BirthDate(DateTime referenceDateTime)
    {
        _referenceDateTime = referenceDateTime;
    }

    public string ByAge(uint age)
    {
        var referenceDateTime = _referenceDateTime ?? DateTime.Now;
        DateTime bithDate = referenceDateTime.AddYears((int)age * -1);
        return bithDate.ToString("d");
    }
}
