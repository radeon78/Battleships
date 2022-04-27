namespace UnitTests.Grids
{
    using Battleships.Domain.Ships;
    using Battleships.Domain.Grids;
    using FluentAssertions;
    using Xunit;
    using System;

    public class StartPointTests
    {
        [Theory]
        [InlineData("a")]
        [InlineData("1")]
        [InlineData("hv")]
        [InlineData("1h")]
        public void ShouldThrowArgumentExceptionWhenDirectionIsWrong(string directionAsText)
        {
            // act
            Action action = () => _ = new StartPoint(new Point(0, 0), directionAsText);

            // assert
            action.Should().ThrowExactly<ArgumentException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldThrowExceptionWhenDirectionIsNullOrEmpty(string directionAsText)
        {
            // act
            Action action = () => _ = new StartPoint(new Point(0, 0), directionAsText);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ShouldThrowExceptionWhenPointIsNull()
        {
            // act
            Action action = () => _ = new StartPoint(null, "H");

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Theory]
        [InlineData("h", ShipPosition.Horizontal)]
        [InlineData("H", ShipPosition.Horizontal)]
        [InlineData("v", ShipPosition.Vertical)]
        [InlineData("V", ShipPosition.Vertical)]
        public void ShouldCreateStartPoint(string directionAsText, ShipPosition expectedShipPosition)
        {
            // act
            var startPoint = new StartPoint(new Point(0, 0), directionAsText);

            // assert
            startPoint.Should().NotBeNull();
            startPoint.ShipPosition.Should().Be(expectedShipPosition);
        }

        [Fact]
        public void ShouldCreateEmptyStartPoint()
        {
            // act
            var startPoint = StartPoint.CreateEmptyStartPoint();

            // assert
            startPoint.Should().NotBeNull();
            startPoint.ShipPosition.Should().Be(ShipPosition.Horizontal);
            startPoint.Point.Should().NotBeNull();
            startPoint.Point.Column.Should().Be(0);
            startPoint.Point.Row.Should().Be(0);
        }
    }
}
