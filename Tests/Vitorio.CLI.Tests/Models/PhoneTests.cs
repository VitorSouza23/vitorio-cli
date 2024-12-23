namespace Vitorio.CLI.Tests;

public sealed class PhoneTests
{
    private const int RandomSeed = 123;
    private readonly Random _random;

    public PhoneTests()
    {
        _random = new(RandomSeed);
    }

    [Fact]
    public void Should_Crete_New_Phone_Number()
    {
        // Given
        var phoneRules = new PhoneRules(0, 0, 9, false);
        var phone = new Phone(_random, phoneRules);

        // When
        var result = phone.New();

        // Then
        result.Should().Be("98455-6914");
    }

    [Fact]
    public void Should_Crete_New_Phone_Number_With_Country_Code_55()
    {
        // Given
        var phoneRules = new PhoneRules(55, 0, 9, false);
        var phone = new Phone(_random, phoneRules);

        // When
        var result = phone.New();

        // Then
        result.Should().Be("+55 98455-6914");
    }

    [Fact]
    public void Should_Crete_New_Phone_Number_With_Number_Of_Digits_3()
    {
        // Given
        var phoneRules = new PhoneRules(0, 0, 3, false);
        var phone = new Phone(_random, phoneRules);

        // When
        var result = phone.New();

        // Then
        result.Should().Be("983");
    }

    [Fact]
    public void Should_Crete_New_Phone_Number_With_DDD_11()
    {
        // Given
        var phoneRules = new PhoneRules(0, 11, 9, false);
        var phone = new Phone(_random, phoneRules);

        // When
        var result = phone.New();

        // Then
        result.Should().Be("(11) 98455-6914");
    }

    [Fact]
    public void Should_Crete_New_Phone_Number_With_No_Formating()
    {
        // Given
        var phoneRules = new PhoneRules(0, 0, 9, NotFormatted: true);
        var phone = new Phone(_random, phoneRules);

        // When
        var result = phone.New();

        // Then
        result.Should().Be("984556914");
    }

    [Fact]
    public void Should_Crete_New_Phone_Number_With_All_Options()
    {
        // Given
        var phoneRules = new PhoneRules(55, 11, 9, NotFormatted: false);
        var phone = new Phone(_random, phoneRules);

        // When
        var result = phone.New();

        // Then
        result.Should().Be("+55 (11) 98455-6914");
    }

    [Fact]
    public void Should_Crete_New_Phone_Number_With_All_Options_Not_Formated()
    {
        // Given
        var phoneRules = new PhoneRules(55, 11, 9, NotFormatted: true);
        var phone = new Phone(_random, phoneRules);

        // When
        var result = phone.New();

        // Then
        result.Should().Be("55 11 984556914");
    }
}