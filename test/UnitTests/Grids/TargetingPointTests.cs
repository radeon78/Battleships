namespace UnitTests.Grids
{
    using Battleships.Domain.Grids;
    using Battleships.Domain.Players;
    using FluentAssertions;
    using System;
    using Xunit;

    public class TargetingPointTests
    {
        [Fact]
        public void ShouldSetMissAnswerAndNotThrow()
        {
            // arrange
            var targetingPoint = new TargetingPoint();

            // act
            Action action = () => targetingPoint.SetAnswer(new Answer(1, Reply.Miss));

            // assert
            action.Should().NotThrow();
        }

        [Fact]
        public void ShouldSetHitAnswerAndNotThrow()
        {
            // arrange
            var targetingPoint = new TargetingPoint();

            // act
            Action action = () => targetingPoint.SetAnswer(new Answer(1, Reply.Hit));

            // assert
            action.Should().NotThrow();
        }

        [Fact]
        public void ShouldSetSunkAnswerAndNotThrow()
        {
            // arrange
            var targetingPoint = new TargetingPoint();

            // act
            Action action = () => targetingPoint.SetAnswer(new Answer(1, Reply.Sunk));

            // assert
            action.Should().NotThrow();
        }
    }
}
