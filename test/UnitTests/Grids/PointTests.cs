namespace UnitTests.Grids;

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
    public void ShouldThrowArgumentExceptionWhenPointIsNotCorrect(string pointAsText)
    {
        // act
        Action action = () => _ = new Point(pointAsText);

        // assert
        action.Should().ThrowExactly<ArgumentException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ShouldThrowExceptionWhenPointIsNullOrEmpty(string pointAsText)
    {
        // act
        Action action = () => _ = new Point(pointAsText);

        // assert
        action.Should().ThrowExactly<ArgumentNullException>();
    }

    [Theory]
    [InlineData("a1", 0, 0)]
    [InlineData("B2", 1, 1)]
    [InlineData("c3", 2, 2)]
    [InlineData("j10", 9, 9)]
    public void ShouldCreatePoint(string pointAsText, int expectedColumn, int expectedRow)
    {
        // act
        var point = new Point(pointAsText);

        // assert
        point.Should().NotBeNull();
        point.Column.Should().Be(expectedColumn);
        point.Row.Should().Be(expectedRow);
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