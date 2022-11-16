namespace Vitorio.CLI.Tests;

public class PasswordTests
{
    private const int RandomSeed = 123;
    private readonly Random _random;

    public PasswordTests()
    {
        _random = new(RandomSeed);
    }

    [Fact]
    public void Should_Create_New_Password()
    {
        // Given
        var password = new Password(_random);

        // When
        var result = password.New();

        // Then
        result.Should().Be("*@04Zdbk");
    }

    [Fact]
    public void Should_Create_New_Password_MinCharacters()
    {
        // Given
        var password = new Password(_random, Password.MIN_LENGTH);

        // When
        var result = password.New();

        // Then
        result.Should().Be("*@0");
    }

    [Fact]
    public void Should_Create_New_Password_MaxCharacters()
    {
        // Given
        var password = new Password(_random, Password.MAX_LENGTH);

        // When
        var result = password.New();

        // Then
        result.Should().Be("*@04ZdbknS@InGf7Nra$lb2a0SV!KjP71fPg#oC3$!In0hmAlH");
    }

    [Theory]
    [InlineData(Password.MAX_LENGTH + 1)]
    [InlineData(Password.MIN_LENGTH - 1)]
    public void Should_Return_Empty_Because_Length_Out_Of_Range(int length)
    {
        // Given
        var password = new Password(_random, length);

        // When
        var result = password.New();

        // Then
        result.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(GetValidRange))]
    public void Shoul_Be_True_Length_In_Range(int length)
    {
        // Given
        var password = new Password(_random, length);

        // When
        var result = password.IsLengthInRange();

        // Then
        result.Should().BeTrue();
    }

    public static IEnumerable<object[]> GetValidRange()
    {
        for (int value = Password.MIN_LENGTH; value <= Password.MAX_LENGTH; value++)
            yield return new object[] { value };
    }
}