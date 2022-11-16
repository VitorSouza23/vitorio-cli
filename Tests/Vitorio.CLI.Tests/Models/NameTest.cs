namespace Vitorio.CLI.Tests;

public sealed class NameTests
{
    private const int RandomSeed = 123;
    private readonly Random _random;

    public NameTests()
    {
        _random = new(RandomSeed);
    }

    [Fact]
    public void Should_Create_New_Name()
    {
        // Given
        var name = new Name(_random);

        // When
        var result = name.New();

        // Then
        result.Should().Be("João Lucas Cordeiro");
    }

    [Fact]
    public void Should_Create_New_Female_Name()
    {
        // Given
        var name = new Name(_random);

        // When
        var result = name.New(NameGender.Feminine);

        // Then
        result.Should().Be("Mariah Cordeiro");
    }

    [Fact]
    public void Should_Create_New_Male_Name()
    {
        // Given
        var name = new Name(_random);

        // When
        var result = name.New(NameGender.Masculine);

        // Then
        result.Should().Be("Hugo Cordeiro");
    }
}