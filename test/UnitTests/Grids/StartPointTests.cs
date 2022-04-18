namespace UnitTests.Grids
{
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
        public void ShouldThrowArgumentNullExceptionWhenDirectionIsNullOrEmpty(string directionAsText)
        {
            // act
            Action action = () => _ = new StartPoint(new Point(0, 0), directionAsText);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenPointIsNull()
        {
            // act
            Action action = () => _ = new StartPoint(null, "H");

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Theory]
        [InlineData("h", Position.Horizontal)]
        [InlineData("H", Position.Horizontal)]
        [InlineData("v", Position.Vertical)]
        [InlineData("V", Position.Vertical)]
        public void ShouldCreateStartPoint(string directionAsText, Position expectedPosition)
        {
            // act
            var startPoint = new StartPoint(new Point(0, 0), directionAsText);

            // assert
            startPoint.Should().NotBeNull();
            startPoint.Position.Should().Be(expectedPosition);
        }

        [Fact]
        public void ShouldCreateEmptyStartPoint()
        {
            // act
            var startPoint = StartPoint.CreateEmptyStartPoint();

            // assert
            startPoint.Should().NotBeNull();
            startPoint.Position.Should().Be(Position.Horizontal);
            startPoint.Point.Should().NotBeNull();
            startPoint.Point.Column.Should().Be(0);
            startPoint.Point.Row.Should().Be(0);
        }
    }
}
