namespace UnitTests.Players
{
    using Battleships.Domain.Grids;
    using Battleships.Domain.Players;
    using Battleships.Domain.PlayRules;
    using FluentAssertions;
    using System;
    using System.Threading;
    using Xunit;

    public class HumanPlayerTests
    {
        [Fact]
        public void ShouldApplyGameRule()
        {
            // arrange
            var getPlaceShipStartPointNumberCalls = 0;
            StartPoint getPlaceShipStartPoint(string message)
            {
                getPlaceShipStartPointNumberCalls++;
                return new StartPoint(new Point(1, 1), Direction.Horizontal);
            }

            var callOutPointOnTargetingGridNumberCalls = 0;
            Point callOutPointOnTargetingGrid(string message)
            {
                callOutPointOnTargetingGridNumberCalls++;
                return new Point(1, 1);
            }

            var printErrorMessageNumberCalls = 0;
            void printErrorMessage(string message) => ++printErrorMessageNumberCalls;

            var rule = new ThreeShipsPlayRule();
            var player = new HumanPlayer(
                "player1",
                getPlaceShipStartPoint,
                callOutPointOnTargetingGrid,
                printErrorMessage);

            // act
            Action action = () => player.ApplyGameRule(rule);

            // assert
            action.Should().NotThrow();
            getPlaceShipStartPointNumberCalls.Should().Be(0);
            callOutPointOnTargetingGridNumberCalls.Should().Be(0);
            printErrorMessageNumberCalls.Should().Be(0);
        }

        [Fact]
        public void ShouldPlaceShipsOnGridWithNoError()
        {
            // arrange
            var getPlaceShipStartPointNumberCalls = 0;
            StartPoint getPlaceShipStartPoint(string message)
            {
                getPlaceShipStartPointNumberCalls++;

                return getPlaceShipStartPointNumberCalls == 2
                    ? new StartPoint(new Point(2, 2), Direction.Horizontal)
                    : getPlaceShipStartPointNumberCalls == 3
                    ? new StartPoint(new Point(5, 5), Direction.Vertical)
                    : new StartPoint(new Point(0, 0), Direction.Horizontal);
            }

            var callOutPointOnTargetingGridNumberCalls = 0;
            Point callOutPointOnTargetingGrid(string message)
            {
                callOutPointOnTargetingGridNumberCalls++;
                return new Point(2, 2);
            }

            var printErrorMessageNumberCalls = 0;
            void printErrorMessage(string message) => ++printErrorMessageNumberCalls;

            var rule = new ThreeShipsPlayRule();
            var player = new HumanPlayer(
                "player1",
                getPlaceShipStartPoint,
                callOutPointOnTargetingGrid,
                printErrorMessage);
            player.ApplyGameRule(rule);

            // act
            Action action = () => player.PlaceShipsOnOceanGrid(CancellationToken.None);

            // assert
            action.Should().NotThrow();
            getPlaceShipStartPointNumberCalls.Should().Be(3);
            callOutPointOnTargetingGridNumberCalls.Should().Be(0);
            printErrorMessageNumberCalls.Should().Be(0);
        }

        [Fact]
        public void ShouldPlaceShipsOnGridWithTwoErrors()
        {
            // arrange
            var getPlaceShipStartPointNumberCalls = 0;
            StartPoint getPlaceShipStartPoint(string message)
            {
                getPlaceShipStartPointNumberCalls++;
                return new StartPoint(new Point(getPlaceShipStartPointNumberCalls, getPlaceShipStartPointNumberCalls), Direction.Horizontal);
            }

            var callOutPointOnTargetingGridNumberCalls = 0;
            Point callOutPointOnTargetingGrid(string message)
            {
                callOutPointOnTargetingGridNumberCalls++;
                return new Point(2, 2);
            }

            var printErrorMessageNumberCalls = 0;
            void printErrorMessage(string message) => ++printErrorMessageNumberCalls;

            var rule = new ThreeShipsPlayRule();
            var player = new HumanPlayer(
                "player1",
                getPlaceShipStartPoint,
                callOutPointOnTargetingGrid,
                printErrorMessage);
            player.ApplyGameRule(rule);

            // act
            Action action = () => player.PlaceShipsOnOceanGrid(CancellationToken.None);

            // assert
            action.Should().NotThrow();
            getPlaceShipStartPointNumberCalls.Should().Be(5);
            callOutPointOnTargetingGridNumberCalls.Should().Be(0);
            printErrorMessageNumberCalls.Should().Be(2);
        }

        [Fact]
        public void ShouldCancelPlaceShipsOnGrid()
        {
            // arrange
            var source = new CancellationTokenSource();
            var token = source.Token;

            var getPlaceShipStartPointNumberCalls = 0;
            StartPoint getPlaceShipStartPoint(string message)
            {
                getPlaceShipStartPointNumberCalls++;
                return new StartPoint(new Point(getPlaceShipStartPointNumberCalls, getPlaceShipStartPointNumberCalls), Direction.Horizontal);
            }

            var callOutPointOnTargetingGridNumberCalls = 0;
            Point callOutPointOnTargetingGrid(string message)
            {
                callOutPointOnTargetingGridNumberCalls++;
                return new Point(2, 2);
            }

            var printErrorMessageNumberCalls = 0;
            void printErrorMessage(string message) => ++printErrorMessageNumberCalls;

            var rule = new ThreeShipsPlayRule();
            var player = new HumanPlayer(
                "player1",
                getPlaceShipStartPoint,
                callOutPointOnTargetingGrid,
                printErrorMessage);
            player.ApplyGameRule(rule);

            // act
            Action action = () => player.PlaceShipsOnOceanGrid(token);
            source.Cancel();

            // assert
            action.Should().NotThrow();
            getPlaceShipStartPointNumberCalls.Should().Be(0);
            callOutPointOnTargetingGridNumberCalls.Should().Be(0);
            printErrorMessageNumberCalls.Should().Be(0);
        }

        [Fact]
        public void ShouldCancelPlaceShipsOnGridAfterOneCall()
        {
            // arrange
            var source = new CancellationTokenSource();
            var token = source.Token;

            var getPlaceShipStartPointNumberCalls = 0;
            StartPoint getPlaceShipStartPoint(string message)
            {
                getPlaceShipStartPointNumberCalls++;
                source.Cancel();
                return new StartPoint(new Point(getPlaceShipStartPointNumberCalls, getPlaceShipStartPointNumberCalls), Direction.Horizontal);
            }

            var callOutPointOnTargetingGridNumberCalls = 0;
            Point callOutPointOnTargetingGrid(string message)
            {
                callOutPointOnTargetingGridNumberCalls++;
                return new Point(2, 2);
            }

            var printErrorMessageNumberCalls = 0;
            void printErrorMessage(string message) => ++printErrorMessageNumberCalls;

            var rule = new ThreeShipsPlayRule();
            var player = new HumanPlayer(
                "player1",
                getPlaceShipStartPoint,
                callOutPointOnTargetingGrid,
                printErrorMessage);
            player.ApplyGameRule(rule);

            // act
            Action action = () => player.PlaceShipsOnOceanGrid(token);

            // assert
            action.Should().NotThrow();
            getPlaceShipStartPointNumberCalls.Should().Be(1);
            callOutPointOnTargetingGridNumberCalls.Should().Be(0);
            printErrorMessageNumberCalls.Should().Be(0);
        }

        [Fact]
        public void ShouldCallOutPointOnTargetingGridWithNoError()
        {
            // arrange
            var getPlaceShipStartPointNumberCalls = 0;
            StartPoint getPlaceShipStartPoint(string message)
            {
                getPlaceShipStartPointNumberCalls++;
                return new StartPoint(new Point(1, 1), Direction.Horizontal);
            }

            var callOutPointOnTargetingGridNumberCalls = 0;
            Point callOutPointOnTargetingGrid(string message)
            {
                callOutPointOnTargetingGridNumberCalls++;
                return new Point(4, 2);
            }

            var printErrorMessageNumberCalls = 0;
            void printErrorMessage(string message) => ++printErrorMessageNumberCalls;

            var rule = new ThreeShipsPlayRule();
            var player = new HumanPlayer(
                "player1",
                getPlaceShipStartPoint,
                callOutPointOnTargetingGrid,
                printErrorMessage);
            player.ApplyGameRule(rule);

            // act
            var point = player.CallOutPointOnTargetingGrid();

            // assert
            point.Should().NotBeNull();
            point.Row.Should().Be(2);
            point.Column.Should().Be(4);
            getPlaceShipStartPointNumberCalls.Should().Be(0);
            callOutPointOnTargetingGridNumberCalls.Should().Be(1);
            printErrorMessageNumberCalls.Should().Be(0);
        }

        [Fact]
        public void ShouldCallOutPointOnTargetingGridWithOneError()
        {
            // arrange
            var getPlaceShipStartPointNumberCalls = 0;
            StartPoint getPlaceShipStartPoint(string message)
            {
                getPlaceShipStartPointNumberCalls++;
                return new StartPoint(new Point(1, 1), Direction.Horizontal);
            }

            var callOutPointOnTargetingGridNumberCalls = 0;
            Point callOutPointOnTargetingGrid(string message)
            {
                callOutPointOnTargetingGridNumberCalls++;
                return callOutPointOnTargetingGridNumberCalls == 1
                    ? new Point(10, 0)
                    : new Point(4, 2);
            }

            var printErrorMessageNumberCalls = 0;
            void printErrorMessage(string message) => ++printErrorMessageNumberCalls;

            var rule = new ThreeShipsPlayRule();
            var player = new HumanPlayer(
                "player1",
                getPlaceShipStartPoint,
                callOutPointOnTargetingGrid,
                printErrorMessage);
            player.ApplyGameRule(rule);

            // act
            var point = player.CallOutPointOnTargetingGrid();

            // assert
            point.Should().NotBeNull();
            point.Row.Should().Be(2);
            point.Column.Should().Be(4);
            getPlaceShipStartPointNumberCalls.Should().Be(0);
            callOutPointOnTargetingGridNumberCalls.Should().Be(2);
            printErrorMessageNumberCalls.Should().Be(1);
        }
    }
}
