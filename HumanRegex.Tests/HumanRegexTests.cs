using FluentAssertions;

namespace HumanRegex.Tests;

public class Tests
{
    
    [Test]
    public void HasDigitShouldReturnTheRegexForDigits()
    {
        var result = HumanRegex.Create().HasDigit().ToRegExp();
        var test = result.Match("123abcdefghijklmnop");
        test.Success.Should().BeTrue();
    }

    [Test]
    public void LiteralsShouldReturnOneOrAnother()
    {
        var result = HumanRegex.Create().Literal("Rice").Or().Literal("Chicken").ToRegExp().Match("Rice or Potatoes");
        result.Value.Should().Be("Rice");
        result.Success.Should().BeTrue();
    }

    [Test]
    public void CheckIfIPIsValid()
    {
        var result = HumanRegex.Create().Ipv4Octet().ToRegExp().Match("0.1.69.420");
        result.Value.Should().Be("0");
    }

    [Test]
    public void EmailShouldBeValid()
    {
        var builder = HumanRegex.Create()
            .StartAnchor()
            .Word()
            .OneOrMore()
            .Literal("@")
            .Word()
            .OneOrMore()
            .StartGroup()
            .Literal(".")
            .Word()
            .OneOrMore()
            .EndGroup()
            .ZeroOrMore()
            .Literal(".")
            .Letter()
            .AtLeast(2)
            .EndAnchor();
        
        var resultText = builder.ToString();
        var test = builder.ToRegExp().Match("test@example.com");

        test.Success.Should().BeTrue();
        test.Value.Should().Be("test@example.com");
        resultText.Should().Be("^\\w+@\\w+(?:\\.\\w+)*\\.[a-zA-Z]{2,}$");
    }
}