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
        [InlineData("h", Direction.Horizontal)]
        [InlineData("H", Direction.Horizontal)]
        [InlineData("v", Direction.Vertical)]
        [InlineData("V", Direction.Vertical)]
        public void ShouldCreateStartPoint(string directionAsText, Direction expectedDirection)
        {
            // act
            var startPoint = new StartPoint(new Point(0, 0), directionAsText);

            // assert
            startPoint.Should().NotBeNull();
            startPoint.Direction.Should().Be(expectedDirection);
        }

        [Fact]
        public void ShouldCreateEmptyStartPoint()
        {
            // act
            var startPoint = StartPoint.CreateEmptyStartPoint();

            // assert
            startPoint.Should().NotBeNull();
            startPoint.Direction.Should().Be(Direction.Horizontal);
            startPoint.Point.Should().NotBeNull();
            startPoint.Point.Column.Should().Be(0);
            startPoint.Point.Row.Should().Be(0);
        }
    }
}
