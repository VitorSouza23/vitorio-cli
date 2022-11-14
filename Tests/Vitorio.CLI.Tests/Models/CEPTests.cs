namespace Vitorio.CLI.Tests;

public sealed class CEPTests
{
    private const int RandomSeed = 123;
    private readonly Random _random;

    public CEPTests()
    {
        _random = new Random(RandomSeed);
    }

    [Fact]
    public void Should_Create_New_CEP()
    {
        // Given
        var cep = new CEP(_random, false);

        // When
        var result = (string)cep.New();

        // Then
        result.Should().Be("98455690");
    }

    [Fact]
    public void Should_Create_New_PutInitializeValue_CEP()
    {
        // Given
        var cep = new CEP(_random);

        // When
        var result = cep.ToString();

        // Then
        result.Should().Be("98455690");
    }

    [Fact]
    public void Should_Create_New_CEP_Formated()
    {
        // Given
        var cep = new CEP(_random);

        // When
        var result = (string)cep.Format();

        // Then
        result.Should().Be("98455-690");
    }

    [Fact]
    public void Should_Create_Empty()
    {
        // Given
        var cep = new CEP(_random, false);

        // When
        var result = cep.ToString();

        // Then
        result.Should().BeEmpty();
    }

    [Fact]
    public void Should_Create_Empty_Formated()
    {
        // Given
        var cep = new CEP(_random, false);

        // When
        var result = cep.Format().ToString();

        // Then
        result.Should().BeEmpty();
    }
}