using System.Text.RegularExpressions;
using Battleships.Domain.Common;
using FluentAssertions;
using Xunit;

namespace UnitTests.Common;

public class RegexPatternsTests
{
    [Theory]
    [InlineData("I123-123S", false)]
    [InlineData("I1-23", false)]
    [InlineData("A1", true)]
    [InlineData("B2", true)]
    [InlineData("C3", true)]
    [InlineData("d4", true)]
    [InlineData("e5", true)]
    [InlineData("f6", true)]
    [InlineData("g7", true)]
    [InlineData("H8", true)]
    [InlineData("I9", true)]
    [InlineData("J10", true)]
    [InlineData("J11", false)]
    [InlineData("A0", false)]
    [InlineData("B11", false)]
    [InlineData("K1", false)]
    [InlineData("K11", false)]
    [InlineData("1", false)]
    [InlineData("B", false)]
    [InlineData("1A", false)]
    [InlineData("AA1", false)]
    [InlineData("A111", false)]
    [InlineData("", false)]
    public void PointPattern(string input, bool expectedIsMatch)
    {
        // act
        var isMatch = Regex.Match(input, RegexPatterns.PointPattern).Success;

        // assert
        isMatch.Should().Be(expectedIsMatch);
    }

    [Theory]
    [InlineData("h", true)]
    [InlineData("H", true)]
    [InlineData("v", true)]
    [InlineData("V", true)]
    [InlineData("h1", false)]
    [InlineData("H2", false)]
    [InlineData("1h", false)]
    [InlineData("1H", false)]
    [InlineData("v1", false)]
    [InlineData("V4", false)]
    [InlineData("1v", false)]
    [InlineData("3V", false)]
    [InlineData("A", false)]
    [InlineData("a", false)]
    [InlineData("b", false)]
    [InlineData("2", false)]
    [InlineData("54", false)]
    [InlineData("", false)]
    public void DirectionPattern(string input, bool expectedIsMatch)
    {
        // act
        var isMatch = Regex.Match(input, RegexPatterns.DirectionPattern).Success;

        // assert
        isMatch.Should().Be(expectedIsMatch);
    }
}