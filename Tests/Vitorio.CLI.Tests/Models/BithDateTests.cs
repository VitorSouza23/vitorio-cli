namespace Vitorio.CLI.Tests;

public sealed class BirthDateTests
{
    private readonly DateTime _referenceDateTime;

    public BirthDateTests()
    {
        _referenceDateTime = new DateTime(2000, 1, 1);
    }

    [Fact]
    public void Should_Create_10_Year_Ago_Date()
    {
        // Given
        var age = 10u;
        var birthDate = new BirthDate(_referenceDateTime);

        // When
        var result = birthDate.ByAge(age);

        // Then
        result.Should().Be(_referenceDateTime.AddYears(-10).ToString("d"));
    }
}