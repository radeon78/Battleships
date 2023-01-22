using Battleships.Domain.Extensions;
using FluentAssertions;
using Xunit;

namespace UnitTests.Extensions;

public class NumberExtensionsTests
{
    [Theory]
    [InlineData(0, 'A')]
    [InlineData(1, 'B')]
    [InlineData(2, 'C')]
    [InlineData(3, 'D')]
    [InlineData(4, 'E')]
    [InlineData(5, 'F')]
    [InlineData(6, 'G')]
    [InlineData(7, 'H')]
    [InlineData(8, 'I')]
    [InlineData(9, 'J')]
    public void ShouldDisplayColumnAsChar(int columnNumber, char expectedChar)
    { 
        // act
        var columnChar = columnNumber.ToDisplayColumn();

        // assert
        columnChar.Should().Be(expectedChar);
    }

    [Theory]
    [InlineData('A', 0)]
    [InlineData('B', 1)]
    [InlineData('C', 2)]
    [InlineData('D', 3)]
    [InlineData('E', 4)]
    [InlineData('F', 5)]
    [InlineData('G', 6)]
    [InlineData('H', 7)]
    [InlineData('I', 8)]
    [InlineData('J', 9)]
    [InlineData('a', 0)]
    [InlineData('b', 1)]
    [InlineData('c', 2)]
    [InlineData('d', 3)]
    [InlineData('e', 4)]
    [InlineData('f', 5)]
    [InlineData('g', 6)]
    [InlineData('h', 7)]
    [InlineData('i', 8)]
    [InlineData('j', 9)]
    public void ShouldConvertColumnCharToNumber(char columnChar, int expectedNumber)
    {
        // act
        var columnNumber = columnChar.ToNumberColumn();

        // assert
        columnNumber.Should().Be(expectedNumber);
    }

    [Theory]
    [InlineData(0, "A")]
    [InlineData(1, "B")]
    [InlineData(2, "C")]
    [InlineData(3, "D")]
    [InlineData(4, "E")]
    [InlineData(5, "F")]
    [InlineData(6, "G")]
    [InlineData(7, "H")]
    [InlineData(8, "I")]
    [InlineData(9, "J")]
    public void ShouldDisplayColumnAsString(int columnNumber, string expectedString)
    {
        // act
        var columnString = columnNumber.ToDisplayColumnAsString();

        // assert
        columnString.Should().Be(expectedString);
    }

    [Theory]
    [InlineData(0, "1")]
    [InlineData(1, "2")]
    [InlineData(2, "3")]
    [InlineData(3, "4")]
    [InlineData(4, "5")]
    [InlineData(5, "6")]
    [InlineData(6, "7")]
    [InlineData(7, "8")]
    [InlineData(8, "9")]
    [InlineData(9, "10")]
    public void ShouldDisplayRowAsText(int rowNumber, string expectedString)
    {
        // act
        var rowString = rowNumber.ToDisplayRow();

        // assert
        rowString.Should().Be(expectedString);
    }
}