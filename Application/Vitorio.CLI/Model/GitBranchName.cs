namespace Vitorio.CLI.Model;

public static class GitBranchName
{
    public static string Format(string input, string prefix = null)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        if (string.IsNullOrWhiteSpace(prefix))
            return ApplyFormat(input);

        var formatedPrefix = ApplyFormat(prefix);
        var formatedInput = ApplyFormat(input);

        return $"{formatedPrefix}/{formatedInput}";

        static string ApplyFormat(string value) => value
            .ToLower()
            .Trim()
            .Replace(" ", "-");
    }
}