namespace UnitTests.Ships
{
    using Battleships.Domain.Ships;
    using FluentAssertions;
    using System;
    using Xunit;

    public class ShipTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void ShouldNotThrow(int length)
        {
            // act
            Action action = () => _ = new Ship(length);

            // assert
            action.Should().NotThrow();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(6)]
        [InlineData(10)]
        [InlineData(-1)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void ShouldThrow(int length)
        {
            // act
            Action action = () => { _ = new Ship(length); };

            // assert
            action.Should().ThrowExactly<ArgumentException>();
        }

        [Theory]
        [InlineData(1, "1 length ship")]
        [InlineData(2, "2 length ship")]
        [InlineData(3, "3 length ship")]
        [InlineData(4, "4 length ship")]
        [InlineData(5, "5 length ship")]
        public void ShouldPrintShip(int length, string expectedResult)
        {
            // act
            var ship = new Ship(length);

            // assert
            ship.ToString().Should().Be(expectedResult);
        }
    }
}
