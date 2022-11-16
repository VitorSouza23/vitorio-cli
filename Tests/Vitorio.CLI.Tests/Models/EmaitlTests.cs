namespace Vitorio.CLI.Tests;

public sealed class EmailTests
{
    private const int RandomSeed = 123;
    private readonly Random _random;

    public EmailTests()
    {
        _random = new(RandomSeed);
    }

    [Fact]
    public void Should_Create_New_Email()
    {
        // Given
        var email = new Email(_random);

        // When
        var result = email.Value;

        // Then
        result.Should().Be("bafhw@96030.com");
    }

    [Fact]
    public void Should_Create_New_Email_With_Provider()
    {
        // Given
        string provider = "test";
        var email = new Email(_random, provider);

        // When
        var result = email.Value;

        // Then
        result.Should().Be("96030@test.com");
    }

    [Fact]
    public void Should_Create_New_Email_With_Domain()
    {
        // Given
        string domain = "test";
        var email = new Email(_random, domain: domain);

        // When
        var result = email.Value;

        // Then
        result.Should().Be("bafhw@96030.test");
    }

    [Fact]
    public void Should_Create_New_Email_With_Provider_And_Domain()
    {
        // Given
        string provider = "test";
        string domain = "test";
        var email = new Email(_random, provider, domain);

        // When
        var result = email.Value;

        // Then
        result.Should().Be("96030@test.test");
    }
}