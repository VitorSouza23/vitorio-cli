namespace Vitorio.CLI.Model;

public static class BirthDate
{
    public static string ByAge(uint age)
    {
        DateTime bithDate = DateTime.Now.AddYears((int)age * -1);
        return bithDate.ToString("d");
    }
}
