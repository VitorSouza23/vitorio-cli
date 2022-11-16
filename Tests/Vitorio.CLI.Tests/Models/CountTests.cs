namespace Vitorio.CLI.Tests;

public sealed class CountTests
{
    [Fact]
    public void Should_Create_DefaultValue()
    {
        // Given
        var count = Count.Default();

        // When
        var result = (int)count;

        // Then
        result.Should().Be(1);
    }

    [Theory]
    [MemberData(nameof(GetCountinRange))]
    public void Should_Count_Between_Max_Min_Values(int countValue)
    {
        // Given
        var count = (Count)countValue;

        // When
        var result = count.IsItOnRange();

        // Then
        result.Should().BeTrue();
    }

    public static IEnumerable<object[]> GetCountinRange()
    {
        for (int count = Count.DefaultMinValue; count <= Count.DefaultMaxValue; count++)
            yield return new object[] { count };
    }

    [Theory]
    [InlineData(Count.DefaultMaxValue + 1)]
    [InlineData(Count.DefaultMinValue - 1)]
    public void Should_Count_Be_Out_Of_Range(int countValue)
    {
        // Given
        var count = (Count)countValue;

        // When
        var result = count.IsItNotOnRange();

        // Then
        result.Should().BeTrue();
    }
}