namespace Vitorio.CLI.Tests;

public class CNPJTests
{
    private readonly Random _random;
    private const int RandomSeed = 123;

    public CNPJTests()
    {
        _random = new(RandomSeed);
    }

    [Fact]
    public void Should_Create_New_CNPJ()
    {
        // Given
        var cnpj = new Cnpj(_random, false);

        // When
        var result = cnpj.New().ToString();

        // Then
        result.Should().Be("98455690000178");
    }

    [Fact]
    public void Should_Create_New_PutInitializeValue_CNPJ()
    {
        // Given
        var cnpj = new Cnpj(_random);

        // When
        var result = cnpj.ToString();

        // Then
        result.Should().Be("98455690000178");
    }

    [Fact]
    public void Should_Create_New_CNPJ_Formated()
    {
        // Given
        var cnpj = new Cnpj(_random);

        // When
        var result = Cnpj.Format(cnpj.ToString());

        // Then
        result.Should().Be("98.455.690/0001-78");
    }

    [Fact]
    public void Should_Remove_Formatting()
    {
        // Given
        var cnpj = "98.455.690/0001-78";

        // When
        var result = Cnpj.RemoveFormat(cnpj);

        // Then
        result.Should().Be("98455690000178");
    }

    [Fact]
    public void Should_Not_Remove_Formatting()
    {
        // Given
        var cnpj = "TEST";

        // When
        var result = Cnpj.RemoveFormat(cnpj);

        // Then
        result.Should().Be("TEST");
    }

    [Fact]
    public void Should_Check_IsCnpj_True()
    {
        // Given
        var cnpj = "98455690000178";

        // When
        var result = Cnpj.IsCnpj(cnpj);

        // Then
        result.Should().BeTrue();
    }

    [Fact]
    public void Should_Check_IsCnpj_True_Formated()
    {
        // Given
        var cnpj = "98.455.690/0001-78";

        // When
        var result = Cnpj.IsCnpj(cnpj);

        // Then
        result.Should().BeTrue();
    }

    [Fact]
    public void Should_Check_IsCnpj_Flase()
    {
        // Given
        var cnpj = "123";

        // When
        var result = Cnpj.IsCnpj(cnpj);

        // Then
        result.Should().BeFalse();
    }
}