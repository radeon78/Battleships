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
            Action action = () => _ = new Ship(length);

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

        [Fact]
        public void ShouldHitShipAndNotThrow()
        {
            // arrange
            var ship = new Ship(2);

            // act
            Action action = () =>
            {
                ship.Hit();
                ship.Hit();
            };

            // assert
            action.Should().NotThrow();
        }

        [Fact]
        public void ShouldHitShipAndThrow()
        {
            // arrange
            var ship = new Ship(2);

            // act
            Action action = () =>
            {
                ship.Hit();
                ship.Hit();
                ship.Hit();
            };

            // assert
            action.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void ShouldCloneShip()
        {
            // arrange
            var ship = new Ship(2);
            ship.Hit();
            var clonedShip = new Ship(ship);

            // act
            var result = ship.Equals(clonedShip);

            // assert
            result.Should().BeTrue();
            ship.GetHashCode().Should().Be(clonedShip.GetHashCode());
        }

        [Fact]
        public void ShouldCompareTheSameShips()
        {
            // act
            var ship1 = new Ship(2);
            var ship2 = new Ship(2);

            // act
            var result = ship1.Equals(ship2);

            // assert
            result.Should().BeTrue();
            ship1.GetHashCode().Should().Be(ship2.GetHashCode());
        }

        [Fact]
        public void ShouldCompareDifferentShips()
        {
            // arrange
            var ship1 = new Ship(2);
            ship1.Hit();
            var ship2 = new Ship(2);

            // act
            var result = ship1.Equals(ship2);

            // assert
            result.Should().BeFalse();
            ship1.GetHashCode().Should().NotBe(ship2.GetHashCode());
        }

        [Fact]
        public void ShouldCompareShipWithNull()
        {
            // arrange
            var ship1 = new Ship(2);
            
            // act
            var result = ship1.Equals(null);

            // assert
            result.Should().BeFalse();
        }
    }
}
