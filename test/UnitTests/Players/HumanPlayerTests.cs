namespace UnitTests.Players
{
    using Battleships.Domain.Grids;
    using Battleships.Domain.Players;
    using Battleships.Domain.PlayRules;
    using FluentAssertions;
    using System;
    using System.Threading;
    using UnitTests.Fakes;
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

            var printOceanGridNumberCalls = 0;
            void printOceanGrid(OceanGrid oceanGrid) => printOceanGridNumberCalls++; ;

            var callOutPointOnTargetingGridNumberCalls = 0;
            Point callOutPointOnTargetingGrid(string message)
            {
                callOutPointOnTargetingGridNumberCalls++;
                return new Point(1, 1);
            }

            var printErrorMessageNumberCalls = 0;
            void printErrorMessage(string message) => ++printErrorMessageNumberCalls;

            var playerName = "player1";

            var rule = new ThreeShipsPlayRule();
            var player = new HumanPlayer(
                playerName,
                getPlaceShipStartPoint,
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printErrorMessage);

            // act
            Action action = () => player.ApplyGameRule(rule);

            // assert
            action.Should().NotThrow();
            getPlaceShipStartPointNumberCalls.Should().Be(0);
            printOceanGridNumberCalls.Should().Be(0);
            callOutPointOnTargetingGridNumberCalls.Should().Be(0);
            printErrorMessageNumberCalls.Should().Be(0);
            player.PlayerName.Should().Be(playerName);
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

            var printOceanGridNumberCalls = 0;
            void printOceanGrid(OceanGrid oceanGrid) => printOceanGridNumberCalls++; ;

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
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printErrorMessage);
            player.ApplyGameRule(rule);

            // act
            Action action = () => player.PlaceShipsOnOceanGrid(CancellationToken.None);

            // assert
            action.Should().NotThrow();
            getPlaceShipStartPointNumberCalls.Should().Be(3);
            printOceanGridNumberCalls.Should().Be(0);
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

            var printOceanGridNumberCalls = 0;
            void printOceanGrid(OceanGrid oceanGrid) => printOceanGridNumberCalls++; ;

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
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printErrorMessage);
            player.ApplyGameRule(rule);

            // act
            Action action = () => player.PlaceShipsOnOceanGrid(CancellationToken.None);

            // assert
            action.Should().NotThrow();
            getPlaceShipStartPointNumberCalls.Should().Be(5);
            printOceanGridNumberCalls.Should().Be(0);
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

            var printOceanGridNumberCalls = 0;
            void printOceanGrid(OceanGrid oceanGrid) => printOceanGridNumberCalls++; ;

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
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printErrorMessage);
            player.ApplyGameRule(rule);

            // act
            Action action = () => player.PlaceShipsOnOceanGrid(token);
            source.Cancel();

            // assert
            action.Should().NotThrow();
            getPlaceShipStartPointNumberCalls.Should().Be(0);
            printOceanGridNumberCalls.Should().Be(0);
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
                source!.Cancel();
                return new StartPoint(new Point(getPlaceShipStartPointNumberCalls, getPlaceShipStartPointNumberCalls), Direction.Horizontal);
            }

            var printOceanGridNumberCalls = 0;
            void printOceanGrid(OceanGrid oceanGrid) => printOceanGridNumberCalls++; ;

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
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printErrorMessage);
            player.ApplyGameRule(rule);

            // act
            Action action = () => player.PlaceShipsOnOceanGrid(token);

            // assert
            action.Should().NotThrow();
            getPlaceShipStartPointNumberCalls.Should().Be(1);
            printOceanGridNumberCalls.Should().Be(0);
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

            var printOceanGridNumberCalls = 0;
            void printOceanGrid(OceanGrid oceanGrid) => printOceanGridNumberCalls++; ;

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
                printOceanGrid,
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
            printOceanGridNumberCalls.Should().Be(0);
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

            var printOceanGridNumberCalls = 0;
            void printOceanGrid(OceanGrid oceanGrid) => printOceanGridNumberCalls++; ;

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
                printOceanGrid,
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
            printOceanGridNumberCalls.Should().Be(0);
            callOutPointOnTargetingGridNumberCalls.Should().Be(2);
            printErrorMessageNumberCalls.Should().Be(1);
        }

        [Theory]
        [InlineData(3, 9, Reply.Hit, 5)]
        [InlineData(4, 9, Reply.Hit, 5)]
        [InlineData(5, 9, Reply.Hit, 5)]
        [InlineData(6, 9, Reply.Hit, 5)]
        [InlineData(7, 9, Reply.Hit, 5)]
        [InlineData(9, 1, Reply.Hit, 4)]
        [InlineData(9, 2, Reply.Hit, 4)]
        [InlineData(9, 3, Reply.Hit, 4)]
        [InlineData(9, 4, Reply.Hit, 4)]
        [InlineData(9, 5, Reply.Miss, 0)]
        [InlineData(8, 5, Reply.Miss, 0)]
        [InlineData(8, 4, Reply.Miss, 0)]
        [InlineData(8, 3, Reply.Miss, 0)]
        [InlineData(8, 2, Reply.Miss, 0)]
        [InlineData(8, 1, Reply.Miss, 0)]
        [InlineData(8, 0, Reply.Miss, 0)]
        [InlineData(9, 0, Reply.Miss, 0)]
        [InlineData(2, 9, Reply.Miss, 0)]
        [InlineData(2, 8, Reply.Miss, 0)]
        [InlineData(3, 8, Reply.Miss, 0)]
        [InlineData(4, 8, Reply.Miss, 0)]
        [InlineData(5, 8, Reply.Miss, 0)]
        [InlineData(6, 8, Reply.Miss, 0)]
        [InlineData(7, 8, Reply.Miss, 0)]
        [InlineData(8, 8, Reply.Miss, 0)]
        [InlineData(8, 9, Reply.Miss, 0)]

        public void ShouldAnswerToAttacker(int column, int row, Reply expectedReply, int expectedShipLength)
        {
            // arrange
            var getPlaceShipStartPointNumberCalls = 0;
            StartPoint getPlaceShipStartPoint(string message)
            {
                getPlaceShipStartPointNumberCalls++;

                return getPlaceShipStartPointNumberCalls == 1
                    ? new StartPoint(new Point(3, 9), Direction.Horizontal)
                    : new StartPoint(new Point(9, 1), Direction.Vertical);
            }

            var printOceanGridNumberCalls = 0;
            void printOceanGrid(OceanGrid oceanGrid) => printOceanGridNumberCalls++; ;

            var callOutPointOnTargetingGridNumberCalls = 0;
            Point callOutPointOnTargetingGrid(string message)
            {
                callOutPointOnTargetingGridNumberCalls++;
                return new Point(2, 2);
            }

            var printErrorMessageNumberCalls = 0;
            void printErrorMessage(string message) => ++printErrorMessageNumberCalls;

            var rule = new FakeTwoShipsPlayRule();
            var player = new HumanPlayer(
                "player1",
                getPlaceShipStartPoint,
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printErrorMessage);
            player.ApplyGameRule(rule);
            player.PlaceShipsOnOceanGrid(CancellationToken.None);

            var point = new Point(column, row);

            // act
            var answer = player.AnswerToAttacker(point);

            // assert
            getPlaceShipStartPointNumberCalls.Should().Be(2);
            printOceanGridNumberCalls.Should().Be(0);
            callOutPointOnTargetingGridNumberCalls.Should().Be(0);
            printErrorMessageNumberCalls.Should().Be(0);

            answer.Should().NotBeNull();
            answer.Reply.Should().Be(expectedReply);
            answer.ShipLength.Should().Be(expectedShipLength);
        }

        [Fact]
        public void ShouldAnswerToAttackerHitAndThenSunk()
        {
            // arrange
            var getPlaceShipStartPointNumberCalls = 0;
            StartPoint getPlaceShipStartPoint(string message)
            {
                getPlaceShipStartPointNumberCalls++;
                return new StartPoint(new Point(4, 4), Direction.Horizontal);
            }

            var printOceanGridNumberCalls = 0;
            void printOceanGrid(OceanGrid oceanGrid) => printOceanGridNumberCalls++; ;

            var callOutPointOnTargetingGridNumberCalls = 0;
            Point callOutPointOnTargetingGrid(string message)
            {
                callOutPointOnTargetingGridNumberCalls++;
                return new Point(2, 2);
            }

            var printErrorMessageNumberCalls = 0;
            void printErrorMessage(string message) => ++printErrorMessageNumberCalls;

            var rule = new FakeOneShipPlayRule();
            var player = new HumanPlayer(
                "player1",
                getPlaceShipStartPoint,
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printErrorMessage);
            player.ApplyGameRule(rule);
            player.PlaceShipsOnOceanGrid(CancellationToken.None);

            var point1 = new Point(4, 4);
            var point2 = new Point(5, 4);

            // act
            var answer1 = player.AnswerToAttacker(point1);
            var answer2 = player.AnswerToAttacker(point2);

            // assert
            getPlaceShipStartPointNumberCalls.Should().Be(1);
            printOceanGridNumberCalls.Should().Be(0);
            callOutPointOnTargetingGridNumberCalls.Should().Be(0);
            printErrorMessageNumberCalls.Should().Be(0);

            answer1.Should().NotBeNull();
            answer1.Reply.Should().Be(Reply.Hit);
            answer1.ShipLength.Should().Be(2);

            answer2.Should().NotBeNull();
            answer2.Reply.Should().Be(Reply.Sunk);
            answer2.ShipLength.Should().Be(2);
        }

        [Fact]
        public void ShouldThrowWhenGetPlaceShipStartPointIsNull()
        {
            // arrange
            void printOceanGrid(OceanGrid oceanGrid) { };
            Point callOutPointOnTargetingGrid(string message) => new Point(1, 1);
            void printErrorMessage(string message) { };

            var playerName = "player1";

            // act
            Action action = () => _ = new HumanPlayer(
                playerName,
                null,
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printErrorMessage);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ShouldThrowWhenPrintOceanGridIsNull()
        {
            // arrange
            StartPoint getPlaceShipStartPoint(string message) => new (new Point(1, 1), Direction.Horizontal);
            Point callOutPointOnTargetingGrid(string message) => new(1, 1);
            void printErrorMessage(string message) { };

            var playerName = "player1";

            // act
            Action action = () => _ = new HumanPlayer(
                playerName,
                getPlaceShipStartPoint,
                null,
                callOutPointOnTargetingGrid,
                printErrorMessage);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ShouldThrowWhenCallOutPointOnTargetingGridIsNull()
        {
            // arrange
            StartPoint getPlaceShipStartPoint(string message) => new(new Point(1, 1), Direction.Horizontal);
            void printOceanGrid(OceanGrid oceanGrid) { };
            void printErrorMessage(string message) { };

            var playerName = "player1";

            // act
            Action action = () => _ = new HumanPlayer(
                playerName,
                getPlaceShipStartPoint,
                printOceanGrid,
                null,
                printErrorMessage);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ShouldThrowWhenPrintErrorMessageIsNull()
        {
            // arrange
            StartPoint getPlaceShipStartPoint(string message) => new(new Point(1, 1), Direction.Horizontal);
            void printOceanGrid(OceanGrid oceanGrid) { };
            Point callOutPointOnTargetingGrid(string message) => new(1, 1);

            var playerName = "player1";

            // act
            Action action = () => _ = new HumanPlayer(
                playerName,
                getPlaceShipStartPoint,
                printOceanGrid,
                callOutPointOnTargetingGrid,
                null);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}
