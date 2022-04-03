namespace UnitTests.Players
{
    using Battleships.Domain.Players;
    using Battleships.Domain.PlayRules;
    using FluentAssertions;
    using System;
    using System.Threading;
    using Xunit;

    public class ComputerPlayerTests
    {
        [Fact]
        public void ShouldApplyGameRule()
        {
            // arrange
            var playerName = "computer";
            var rule = new ThreeShipsPlayRule();
            var player = new ComputerPlayer(playerName);

            // act
            Action action = () => player.ApplyGameRule(rule);

            // assert
            action.Should().NotThrow();
            player.PlayerName.Should().Be(playerName);
        }

        [Fact]
        public void ShouldPlaceShipsOnGrid()
        {
            // arrange
            var rule = new ThreeShipsPlayRule();
            var player = new ComputerPlayer("computer");
            player.ApplyGameRule(rule);

            // act
            Action action = () => player.PlaceShipsOnOceanGrid(CancellationToken.None);

            // assert
            action.Should().NotThrow();
        }

        [Fact]
        public void ShouldCancelPlaceShipsOnGrid()
        {
            // arrange
            var rule = new ThreeShipsPlayRule();
            var player = new ComputerPlayer("computer");
            player.ApplyGameRule(rule);

            var source = new CancellationTokenSource();
            var token = source.Token;

            // act
            Action action = () => player.PlaceShipsOnOceanGrid(token);
            source.Cancel();

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

        [Fact]
        public void ShouldCallOutPointOnTargetingGrid()
        {
            // arrange
            var player = new ComputerPlayer("computer");

            // act
            var point = player.CallOutPointOnTargetingGrid();

            // assert
            point.Column.Should().BeGreaterThanOrEqualTo(0);
            point.Column.Should().BeLessThanOrEqualTo(9);
            point.Row.Should().BeGreaterThanOrEqualTo(0);
            point.Row.Should().BeLessThanOrEqualTo(9);
        }
    }
}
