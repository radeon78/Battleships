namespace UnitTests.Grids
{
    using Battleships.Domain.Grids;
    using Battleships.Domain.Players;
    using FluentAssertions;
    using System;
    using Xunit;

    public class TargetingGridTests
    {

        [Fact]
        public void ShouldSetAnswerAndNotThrow()
        {
            // arrange
            var targetingGrid = new TargetingGrid();
            var attackerPoint = new Point(1, 4);
            var answer = new Answer(2, Reply.Miss);

            // act
            Action action = () => targetingGrid.SetAnswer(attackerPoint, answer);

            // assert
            action.Should().NotThrow();
        }
    }
}
