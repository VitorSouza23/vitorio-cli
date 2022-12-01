namespace Vitorio.CLI.Tests;

public sealed class GitBranchNameTests
{
    [Fact]
    public void Should_Format_Branch_Name()
    {
        // Given
        var input = "This is a test";

        // When
        var result = GitBranchName.Format(input);

        // Then
        result.Should().Be("this-is-a-test");
    }

    [Fact]
    public void Should_Format_Branch_Name_With_Prefix()
    {
        // Given
        var input = "This is a test";
        var prefix = "This is a prefix";

        // When
        var result = GitBranchName.Format(input, prefix);

        // Then
        result.Should().Be("this-is-a-prefix/this-is-a-test");
    }

    [Fact]
    public void Should_Return_String_Empty_Because_Empty_Input()
    {
        // Given

        // When
        var result = GitBranchName.Format(null);

        // Then
        result.Should().BeEmpty();
    }

    [Fact]
    public void Should_Return_String_Empty_Because_Empty_Input_With_Prefix()
    {
        // Given
        var prefix = "This is a prefix";

        // When
        var result = GitBranchName.Format(null, prefix);

        // Then
        result.Should().BeEmpty();
    }
}