namespace UnitTests.Grids
{
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

        [Fact]
        public void ShouldCloneTargetingPoint()
        {
            // arrange
            var targetingPoint = new TargetingPoint();
            targetingPoint.SetAnswer(new Answer(2, Reply.Hit));
            var clonedTargetingPoint = new TargetingPoint(targetingPoint);

            // act
            var result = targetingPoint.Equals(clonedTargetingPoint);

            // assert
            result.Should().BeTrue();
            targetingPoint.GetHashCode().Should().Be(clonedTargetingPoint.GetHashCode());
        }

        [Fact]
        public void ShouldCompareTargetingPoints()
        {
            // act
            var targetingPoint1 = new TargetingPoint();
            targetingPoint1.SetAnswer(new Answer(2, Reply.Hit));

            var targetingPoint2 = new TargetingPoint();
            targetingPoint2.SetAnswer(new Answer(2, Reply.Hit));

            // act
            var result = targetingPoint1.Equals(targetingPoint2);

            // assert
            result.Should().BeTrue();
            targetingPoint1.GetHashCode().Should().Be(targetingPoint2.GetHashCode());
        }

        [Fact]
        public void ShouldCompareDifferentTargetingPoints()
        {
            // act
            var targetingPoint1 = new TargetingPoint();
            targetingPoint1.SetAnswer(new Answer(2, Reply.Hit));

            var targetingPoint2 = new TargetingPoint();
            targetingPoint2.SetAnswer(new Answer(3, Reply.Hit));

            // act
            var result = targetingPoint1.Equals(targetingPoint2);

            // assert
            result.Should().BeFalse();
            targetingPoint1.GetHashCode().Should().NotBe(targetingPoint2.GetHashCode());
        }

        [Fact]
        public void ShouldCompareTargetingPointWithNull()
        {
            // act
            var targetingPoint = new TargetingPoint();

            // act
            var result = targetingPoint.Equals(null);

            // assert
            result.Should().BeFalse();
        }
    }
}
