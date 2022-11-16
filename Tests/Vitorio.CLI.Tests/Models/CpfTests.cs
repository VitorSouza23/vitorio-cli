namespace Vitorio.CLI.Tests;

public sealed class CpfTests
{
    private const int RandomSeed = 123;
    private readonly Random _random;

    public CpfTests()
    {
        _random = new(RandomSeed);
    }

    [Fact]
    public void Should_Create_New_CPF()
    {
        // Given
        var cpf = new Cpf(_random, false);

        // When
        var result = (string)cpf.New();

        // Then
        result.Should().Be("98455691450");
    }

    [Fact]
    public void Should_Create_New_InitializedValue_CPF()
    {
        // Given
        var cpf = new Cpf(_random);

        // When
        var result = (string)cpf;

        // Then
        result.Should().Be("98455691450");
    }

    [Fact]
    public void Should_Create_New_Formated_CPF()
    {
        // Given
        var cpf = new Cpf(_random);

        // When
        var result = (string)cpf.Format();

        // Then
        result.Should().Be("984.556.914-50");
    }

    [Fact]
    public void Should_Create_New_Empty_CPF()
    {
        // Given
        var cpf = new Cpf(_random, false);

        // When
        var result = (string)cpf;

        // Then
        result.Should().BeEmpty();
    }

    [Fact]
    public void Should_Create_New_Empty_Formated_CPF()
    {
        // Given
        var cpf = new Cpf(_random, false);

        // When
        var result = (string)cpf.Format();

        // Then
        result.Should().BeEmpty();
    }

    [Fact]
    public void Should_Be_Cpf()
    {
        // Given
        var cpf = "98455691450";

        // When
        var result = Cpf.IsCPF(cpf);

        // Then
        result.Should().BeTrue();
    }

    [Fact]
    public void Should_Be_Cpf_Formated()
    {
        // Given
        var cpf = "984.556.914-50";

        // When
        var result = Cpf.IsCPF(cpf);

        // Then
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("123")]
    public void Should_Not_Be_Cpf(string cpf)
    {
        // Given

        // When
        var result = Cpf.IsCPF(cpf);

        // Then
        result.Should().BeFalse();
    }
}