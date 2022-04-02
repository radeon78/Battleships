namespace UnitTests.Players
{
    using Battleships.Domain.Players;
    using Battleships.Domain.PlayRules;
    using FluentAssertions;
    using System;
    using Xunit;

    public class ComputerPlayerTests
    {
        [Fact]
        public void ShouldApplyGameRule()
        {
            // arrange
            var rule = new ThreeShipsPlayRule();
            var player = new ComputerPlayer("computer");

            // act
            Action action = () => player.ApplyGameRule(rule);

            // assert
            action.Should().NotThrow();
        }

        [Fact]
        public void ShouldPlaceShipsOnGrid()
        {
            // arrange
            var rule = new ThreeShipsPlayRule();
            var player = new ComputerPlayer("computer");
            player.ApplyGameRule(rule);

            // act
            Action action = () => player.PlaceShipsOnGrid();

            // assert
            action.Should().NotThrow();
        }

        [Fact]
        public void ShouldGenerateRandomPlaceShipStartPoint()
        {
            // arrange
            var player = new ComputerPlayer("computer");

            // act
            var startPoint = player.GenerateRandomPlaceShipStartPoint();

            // assert
            startPoint.Point.Column.Should().BeGreaterThanOrEqualTo(0);
            startPoint.Point.Column.Should().BeLessThanOrEqualTo(9);
            startPoint.Point.Row.Should().BeGreaterThanOrEqualTo(0);
            startPoint.Point.Row.Should().BeLessThanOrEqualTo(9);
        }
    }
}
