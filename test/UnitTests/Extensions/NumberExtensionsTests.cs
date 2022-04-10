namespace UnitTests.Extensions
{
    using Battleships.Domain.Extensions;
    using FluentAssertions;
    using Xunit;

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
        public void ShouldDisplayColumnAsChar(int columnNumber, char expectedText)
        { 
            // act
            var columnChar = columnNumber.ToDisplayColumn();

            // assert
            columnChar.Should().Be(expectedText);
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
        public void ShouldDisplayColumnAsString(int columnNumber, string expectedText)
        {
            // act
            var columnChar = columnNumber.ToDisplayColumnAsString();

            // assert
            columnChar.Should().Be(expectedText);
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
        public void ShouldDisplayRowAsText(int rowNumber, string expectedText)
        {
            // act
            var columnChar = rowNumber.ToDisplayRow();

            // assert
            columnChar.Should().Be(expectedText);
        }
    }
}
