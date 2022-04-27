namespace UnitTests.Grids;

using Battleships.Domain.Grids;
using Battleships.Domain.Players;
using FluentAssertions;
using Xunit;

public class TargetingPointTests
{
    [Fact]
    public void ShouldMiss()
    {
        // arrange
        var targetingPoint = new TargetingPoint();

        // act
        targetingPoint.SetAnswer(new Answer(1, Reply.Miss));

        // assert
        targetingPoint.Hit().Should().BeFalse();
        targetingPoint.Miss().Should().BeTrue();
        targetingPoint.DisplayShipLength().Should().Be("0");
    }

    [Fact]
    public void ShouldFalseIfAnswerNotSet()
    {
        // act
        var targetingPoint = new TargetingPoint();

        // assert
        targetingPoint.Hit().Should().BeFalse();
        targetingPoint.Miss().Should().BeFalse();
        targetingPoint.DisplayShipLength().Should().Be("0");
    }

    [Fact]
    public void ShouldHit()
    {
        // arrange
        var targetingPoint = new TargetingPoint();

        // act
        targetingPoint.SetAnswer(new Answer(1, Reply.Hit));

        // assert
        targetingPoint.Hit().Should().BeTrue();
        targetingPoint.Miss().Should().BeFalse();
        targetingPoint.DisplayShipLength().Should().Be("1");
    }

    [Fact]
    public void ShouldSunk()
    {
        // arrange
        var targetingPoint = new TargetingPoint();

        // act
        targetingPoint.SetAnswer(new Answer(5, Reply.Sunk));

        // assert
        targetingPoint.Hit().Should().BeTrue();
        targetingPoint.Miss().Should().BeFalse();
        targetingPoint.DisplayShipLength().Should().Be("5");
    }
}