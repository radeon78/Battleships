namespace UnitTests.Grids
{
    using FluentAssertions;
    using Xunit;
    using Battleships.Domain.Grids;
    using System;

    public class PointTests
    {

        [Theory]
        [InlineData("a12")]
        [InlineData("M1")]
        [InlineData("1")]
        [InlineData("A")]
        public void ShouldThrowArgumentException(string pointAsText)
        {
            // act
            Action action = () => _ = new Point(pointAsText);

            // assert
            action.Should().ThrowExactly<ArgumentException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldThrowArgumentNullException(string pointAsText)
        {
            // act
            Action action = () => _ = new Point(pointAsText);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Theory]
        [InlineData("a1")]
        [InlineData("B2")]
        [InlineData("c3")]
        [InlineData("j10")]
        public void ShouldNotThrow(string pointAsText)
        {
            // act
            Action action = () => _ = new Point(pointAsText);

            // assert
            action.Should().NotThrow();
        }

        [Fact]
        public void ShouldCreateEmptyPoint()
        {
            // act
            var point = Point.CreateEmptyPoint();

            // assert
            point.Should().NotBeNull();
            point.Column.Should().Be(0);
            point.Row.Should().Be(0);
        }
    }
}
