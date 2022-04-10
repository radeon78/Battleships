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

        [Fact]
        public void ShouldCloneTargetingGrid()
        {
            // arrange
            var targetingGrid = new TargetingGrid();
            targetingGrid.SetAnswer(new Point(2, 2), new Answer(2, Reply.Hit));
            
            var clonedTargetingGrid = new TargetingGrid(targetingGrid);

            // act
            var result = targetingGrid.Equals(clonedTargetingGrid);

            // asert
            result.Should().BeTrue();
            targetingGrid.GetHashCode().Should().Be(clonedTargetingGrid.GetHashCode());
        }

        [Fact]
        public void ShouldCompareTheSameTargetingGrids()
        {
            // arrange
            var targetingGrid1 = new TargetingGrid();
            targetingGrid1.SetAnswer(new Point(2, 2), new Answer(2, Reply.Hit));
            var targetingGrid2 = new TargetingGrid();
            targetingGrid2.SetAnswer(new Point(2, 2), new Answer(2, Reply.Hit));

            // act
            var result = targetingGrid1.Equals(targetingGrid2);

            // asert
            result.Should().BeTrue();
            targetingGrid1.GetHashCode().Should().Be(targetingGrid2.GetHashCode());
        }

        [Fact]
        public void ShouldCompareDifferentTargetingGrids()
        {
            // arrange
            var targetingGrid1 = new TargetingGrid();
            targetingGrid1.SetAnswer(new Point(2, 2), new Answer(2, Reply.Hit));
            var targetingGrid2 = new TargetingGrid();
            targetingGrid2.SetAnswer(new Point(2, 2), new Answer(3, Reply.Hit));

            // act
            var result = targetingGrid1.Equals(targetingGrid2);

            // asert
            result.Should().BeFalse();
            targetingGrid1.GetHashCode().Should().NotBe(targetingGrid2.GetHashCode());
        }

        [Fact]
        public void ShouldCompareTargetingGridWithNull()
        {
            // arrange
            var targetingGrid1 = new TargetingGrid();

            // act
            var result = targetingGrid1.Equals(null);

            // asert
            result.Should().BeFalse();
        }
    }
}
