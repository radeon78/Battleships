namespace UnitTests.Players
{
    using Battleships.Domain.Grids;
    using Battleships.Domain.Players;
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
            var printOceanGridNumberCalls = 0;
            var callOutPointOnTargetingGridNumberCalls = 0;
            var printTargetingGridNumberCalls = 0;
            var printErrorMessageNumberCalls = 0;
            var playerName = "humanPlayer";

            var rule = new FakeThreeShipsGameRule();
            var player = new HumanPlayer(
                playerName,
                getPlaceShipStartPoint,
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printTargetingGrid,
                printErrorMessage);

            // act
            Action action = () => player.ApplyGameRule(rule);

            // assert
            action.Should().NotThrow();
            getPlaceShipStartPointNumberCalls.Should().Be(0);
            printOceanGridNumberCalls.Should().Be(0);
            callOutPointOnTargetingGridNumberCalls.Should().Be(0);
            printTargetingGridNumberCalls.Should().Be(0);
            printErrorMessageNumberCalls.Should().Be(0);
            player.PlayerName.Should().Be(playerName);

            StartPoint getPlaceShipStartPoint(string message)
            {
                getPlaceShipStartPointNumberCalls++;
                return new StartPoint(new Point(1, 1), Direction.Horizontal);
            }

            void printOceanGrid(string playerName, OceanGrid oceanGrid) => printOceanGridNumberCalls++;
            Point callOutPointOnTargetingGrid(string message)
            {
                callOutPointOnTargetingGridNumberCalls++;
                return new Point(1, 1);
            }

            void printTargetingGrid(string playerName, TargetingGrid targetingGrid) => printTargetingGridNumberCalls++;
            void printErrorMessage(string message) => ++printErrorMessageNumberCalls;
        }

        [Fact]
        public void ShouldPlaceShipsOnGridWithNoError()
        {
            // arrange
            var getPlaceShipStartPointNumberCalls = 0;
            var printOceanGridNumberCalls = 0;
            var callOutPointOnTargetingGridNumberCalls = 0;
            var printTargetingGridNumberCalls = 0;
            var printErrorMessageNumberCalls = 0;

            var rule = new FakeThreeShipsGameRule();
            var player = new HumanPlayer(
                "humanPlayer",
                getPlaceShipStartPoint,
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printTargetingGrid,
                printErrorMessage);
            player.ApplyGameRule(rule);

            // act
            Action action = () => player.PlaceShipsOnOceanGrid(CancellationToken.None);

            // assert
            action.Should().NotThrow();
            getPlaceShipStartPointNumberCalls.Should().Be(3);
            printOceanGridNumberCalls.Should().Be(3);
            callOutPointOnTargetingGridNumberCalls.Should().Be(0);
            printTargetingGridNumberCalls.Should().Be(0);
            printErrorMessageNumberCalls.Should().Be(0);

            StartPoint getPlaceShipStartPoint(string message)
            {
                getPlaceShipStartPointNumberCalls++;

                return getPlaceShipStartPointNumberCalls == 2
                    ? new StartPoint(new Point(2, 2), Direction.Horizontal)
                    : getPlaceShipStartPointNumberCalls == 3
                    ? new StartPoint(new Point(5, 5), Direction.Vertical)
                    : new StartPoint(new Point(0, 0), Direction.Horizontal);
            }

            void printOceanGrid(string playerName, OceanGrid oceanGrid) => printOceanGridNumberCalls++;
            Point callOutPointOnTargetingGrid(string message)
            {
                callOutPointOnTargetingGridNumberCalls++;
                return new Point(2, 2);
            }

            void printTargetingGrid(string playerName, TargetingGrid targetingGrid) => printTargetingGridNumberCalls++;
            void printErrorMessage(string message) => ++printErrorMessageNumberCalls;
        }

        [Fact]
        public void ShouldPlaceShipsOnGridWithTwoErrors()
        {
            // arrange
            var getPlaceShipStartPointNumberCalls = 0;
            var printOceanGridNumberCalls = 0;
            var callOutPointOnTargetingGridNumberCalls = 0;
            var printTargetingGridNumberCalls = 0;
            var printErrorMessageNumberCalls = 0;

            var rule = new FakeThreeShipsGameRule();
            var player = new HumanPlayer(
                "humanPlayer",
                getPlaceShipStartPoint,
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printTargetingGrid,
                printErrorMessage);
            player.ApplyGameRule(rule);

            // act
            Action action = () => player.PlaceShipsOnOceanGrid(CancellationToken.None);

            // assert
            action.Should().NotThrow();
            getPlaceShipStartPointNumberCalls.Should().Be(5);
            printOceanGridNumberCalls.Should().Be(3);
            callOutPointOnTargetingGridNumberCalls.Should().Be(0);
            printTargetingGridNumberCalls.Should().Be(0);
            printErrorMessageNumberCalls.Should().Be(2);

            StartPoint getPlaceShipStartPoint(string message)
            {
                getPlaceShipStartPointNumberCalls++;
                return new StartPoint(new Point(getPlaceShipStartPointNumberCalls, getPlaceShipStartPointNumberCalls), Direction.Horizontal);
            }

            void printOceanGrid(string playerName, OceanGrid oceanGrid) => printOceanGridNumberCalls++;
            Point callOutPointOnTargetingGrid(string message)
            {
                callOutPointOnTargetingGridNumberCalls++;
                return new Point(2, 2);
            }

            void printTargetingGrid(string playerName, TargetingGrid targetingGrid) => printTargetingGridNumberCalls++;
            void printErrorMessage(string message) => ++printErrorMessageNumberCalls;
        }

        [Fact]
        public void ShouldCancelPlaceShipsOnGrid()
        {
            // arrange
            var source = new CancellationTokenSource();
            var token = source.Token;

            var getPlaceShipStartPointNumberCalls = 0;
            var printOceanGridNumberCalls = 0;
            var callOutPointOnTargetingGridNumberCalls = 0;
            var printTargetingGridNumberCalls = 0;
            var printErrorMessageNumberCalls = 0;

            var rule = new FakeThreeShipsGameRule();
            var player = new HumanPlayer(
                "humanPlayer",
                getPlaceShipStartPoint,
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printTargetingGrid,
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
            printTargetingGridNumberCalls.Should().Be(0);
            printErrorMessageNumberCalls.Should().Be(0);

            StartPoint getPlaceShipStartPoint(string message)
            {
                getPlaceShipStartPointNumberCalls++;
                return new StartPoint(new Point(getPlaceShipStartPointNumberCalls, getPlaceShipStartPointNumberCalls), Direction.Horizontal);
            }

            void printOceanGrid(string playerName, OceanGrid oceanGrid) => printOceanGridNumberCalls++;
            Point callOutPointOnTargetingGrid(string message)
            {
                callOutPointOnTargetingGridNumberCalls++;
                return new Point(2, 2);
            }

            void printTargetingGrid(string playerName, TargetingGrid targetingGrid) => printTargetingGridNumberCalls++;
            void printErrorMessage(string message) => ++printErrorMessageNumberCalls;
        }

        [Fact]
        public void ShouldCancelPlaceShipsOnGridAfterOneCall()
        {
            // arrange
            var source = new CancellationTokenSource();
            var token = source.Token;

            var getPlaceShipStartPointNumberCalls = 0;
            var printOceanGridNumberCalls = 0;
            var callOutPointOnTargetingGridNumberCalls = 0;
            var printTargetingGridNumberCalls = 0;
            var printErrorMessageNumberCalls = 0;

            var rule = new FakeThreeShipsGameRule();
            var player = new HumanPlayer(
                "humanPlayer",
                getPlaceShipStartPoint,
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printTargetingGrid,
                printErrorMessage);
            player.ApplyGameRule(rule);

            // act
            Action action = () => player.PlaceShipsOnOceanGrid(token);

            // assert
            action.Should().NotThrow();
            getPlaceShipStartPointNumberCalls.Should().Be(1);
            printOceanGridNumberCalls.Should().Be(1);
            callOutPointOnTargetingGridNumberCalls.Should().Be(0);
            printTargetingGridNumberCalls.Should().Be(0);
            printErrorMessageNumberCalls.Should().Be(0);

            StartPoint getPlaceShipStartPoint(string message)
            {
                getPlaceShipStartPointNumberCalls++;
                source!.Cancel();
                return new StartPoint(new Point(getPlaceShipStartPointNumberCalls, getPlaceShipStartPointNumberCalls), Direction.Horizontal);
            }

            void printOceanGrid(string playerName, OceanGrid oceanGrid) => printOceanGridNumberCalls++;
            Point callOutPointOnTargetingGrid(string message)
            {
                callOutPointOnTargetingGridNumberCalls++;
                return new Point(2, 2);
            }

            void printTargetingGrid(string playerName, TargetingGrid targetingGrid) => printTargetingGridNumberCalls++;
            void printErrorMessage(string message) => ++printErrorMessageNumberCalls;
        }

        [Fact]
        public void ShouldCallOutPointOnTargetingGridWithNoError()
        {
            // arrange
            var getPlaceShipStartPointNumberCalls = 0;
            var printOceanGridNumberCalls = 0;
            var callOutPointOnTargetingGridNumberCalls = 0;
            var printTargetingGridNumberCalls = 0;
            var printErrorMessageNumberCalls = 0;

            var rule = new FakeThreeShipsGameRule();
            var player = new HumanPlayer(
                "humanPlayer",
                getPlaceShipStartPoint,
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printTargetingGrid,
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
            printTargetingGridNumberCalls.Should().Be(0);
            printErrorMessageNumberCalls.Should().Be(0);

            StartPoint getPlaceShipStartPoint(string message)
            {
                getPlaceShipStartPointNumberCalls++;
                return new StartPoint(new Point(1, 1), Direction.Horizontal);
            }

            void printOceanGrid(string playerName, OceanGrid oceanGrid) => printOceanGridNumberCalls++;
            Point callOutPointOnTargetingGrid(string message)
            {
                callOutPointOnTargetingGridNumberCalls++;
                return new Point(4, 2);
            }

            void printTargetingGrid(string playerName, TargetingGrid targetingGrid) => printTargetingGridNumberCalls++;
            void printErrorMessage(string message) => ++printErrorMessageNumberCalls;
        }

        [Fact]
        public void ShouldCallOutPointOnTargetingGridWithOneError()
        {
            // arrange
            var getPlaceShipStartPointNumberCalls = 0;
            var printOceanGridNumberCalls = 0;
            var callOutPointOnTargetingGridNumberCalls = 0;
            var printTargetingGridNumberCalls = 0;
            var printErrorMessageNumberCalls = 0;

            var rule = new FakeThreeShipsGameRule();
            var player = new HumanPlayer(
                "humanPlayer",
                getPlaceShipStartPoint,
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printTargetingGrid,
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
            printTargetingGridNumberCalls.Should().Be(0);
            printErrorMessageNumberCalls.Should().Be(1);

            StartPoint getPlaceShipStartPoint(string message)
            {
                getPlaceShipStartPointNumberCalls++;
                return new StartPoint(new Point(1, 1), Direction.Horizontal);
            }

            void printOceanGrid(string playerName, OceanGrid oceanGrid) => printOceanGridNumberCalls++;
            Point callOutPointOnTargetingGrid(string message)
            {
                callOutPointOnTargetingGridNumberCalls++;
                return callOutPointOnTargetingGridNumberCalls == 1
                    ? new Point(10, 0)
                    : new Point(4, 2);
            }

            void printTargetingGrid(string playerName, TargetingGrid targetingGrid) => printTargetingGridNumberCalls++;
            void printErrorMessage(string message) => ++printErrorMessageNumberCalls;
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
            var printOceanGridNumberCalls = 0;
            var callOutPointOnTargetingGridNumberCalls = 0;
            var printTargetingGridNumberCalls = 0;
            var printErrorMessageNumberCalls = 0;

            var rule = new FakeTwoShipsGameRule();
            var player = new HumanPlayer(
                "humanPlayer",
                getPlaceShipStartPoint,
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printTargetingGrid,
                printErrorMessage);
            player.ApplyGameRule(rule);
            player.PlaceShipsOnOceanGrid(CancellationToken.None);

            var point = new Point(column, row);

            // act
            var answer = player.AnswerToAttacker(point);

            // assert
            getPlaceShipStartPointNumberCalls.Should().Be(2);
            printOceanGridNumberCalls.Should().Be(2);
            callOutPointOnTargetingGridNumberCalls.Should().Be(0);
            printTargetingGridNumberCalls.Should().Be(0);
            printErrorMessageNumberCalls.Should().Be(0);

            answer.Should().NotBeNull();
            answer.Reply.Should().Be(expectedReply);
            answer.ShipLength.Should().Be(expectedShipLength);

            StartPoint getPlaceShipStartPoint(string message)
            {
                getPlaceShipStartPointNumberCalls++;

                return getPlaceShipStartPointNumberCalls == 1
                    ? new StartPoint(new Point(3, 9), Direction.Horizontal)
                    : new StartPoint(new Point(9, 1), Direction.Vertical);
            }

            void printOceanGrid(string playerName, OceanGrid oceanGrid) => printOceanGridNumberCalls++;
            Point callOutPointOnTargetingGrid(string message)
            {
                callOutPointOnTargetingGridNumberCalls++;
                return new Point(2, 2);
            }

            void printTargetingGrid(string playerName, TargetingGrid targetingGrid) => printTargetingGridNumberCalls++;
            void printErrorMessage(string message) => ++printErrorMessageNumberCalls;
        }

        [Fact]
        public void ShouldAnswerToAttackerHitAndThenSunk()
        {
            // arrange
            var getPlaceShipStartPointNumberCalls = 0;
            var printOceanGridNumberCalls = 0;
            var callOutPointOnTargetingGridNumberCalls = 0;
            var printTargetingGridNumberCalls = 0;
            var printErrorMessageNumberCalls = 0;

            var rule = new FakeOneShipGameRule();
            var player = new HumanPlayer(
                "humanPlayer",
                getPlaceShipStartPoint,
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printTargetingGrid,
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
            printOceanGridNumberCalls.Should().Be(1);
            callOutPointOnTargetingGridNumberCalls.Should().Be(0);
            printTargetingGridNumberCalls.Should().Be(0);
            printErrorMessageNumberCalls.Should().Be(0);

            answer1.Should().NotBeNull();
            answer1.Reply.Should().Be(Reply.Hit);
            answer1.ShipLength.Should().Be(2);

            answer2.Should().NotBeNull();
            answer2.Reply.Should().Be(Reply.Sunk);
            answer2.ShipLength.Should().Be(2);

            StartPoint getPlaceShipStartPoint(string message)
            {
                getPlaceShipStartPointNumberCalls++;
                return new StartPoint(new Point(4, 4), Direction.Horizontal);
            }

            void printOceanGrid(string playerName, OceanGrid oceanGrid) => printOceanGridNumberCalls++;
            Point callOutPointOnTargetingGrid(string message)
            {
                callOutPointOnTargetingGridNumberCalls++;
                return new Point(2, 2);
            }

            void printTargetingGrid(string playerName, TargetingGrid targetingGrid) => printTargetingGridNumberCalls++;
            void printErrorMessage(string message) => ++printErrorMessageNumberCalls;
        }

        [Fact]
        public void ShouldThrowWhenGetPlaceShipStartPointIsNull()
        {
            // arrange
            var playerName = "humanPlayer";

            // act
            Action action = () => _ = new HumanPlayer(
                playerName,
                null,
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printTargetingGrid,
                printErrorMessage);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();

            void printOceanGrid(string playerName, OceanGrid oceanGrid) { };
            Point callOutPointOnTargetingGrid(string message) => new(1, 1);
            void printTargetingGrid(string playerName, TargetingGrid targetingGrid) { };
            void printErrorMessage(string message) { };
        }

        [Fact]
        public void ShouldThrowWhenPrintOceanGridIsNull()
        {
            // arrange
            var playerName = "humanPlayer";

            // act
            Action action = () => _ = new HumanPlayer(
                playerName,
                getPlaceShipStartPoint,
                null,
                callOutPointOnTargetingGrid,
                printTargetingGrid,
                printErrorMessage);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();

            StartPoint getPlaceShipStartPoint(string message) => new(new Point(1, 1), Direction.Horizontal);
            Point callOutPointOnTargetingGrid(string message) => new(1, 1);
            void printTargetingGrid(string playerName, TargetingGrid targetingGrid) { };
            void printErrorMessage(string message) { };
        }

        [Fact]
        public void ShouldThrowWhenCallOutPointOnTargetingGridIsNull()
        {
            // arrange
            var playerName = "humanPlayer";

            // act
            Action action = () => _ = new HumanPlayer(
                playerName,
                getPlaceShipStartPoint,
                printOceanGrid,
                null,
                printTargetingGrid,
                printErrorMessage);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();

            StartPoint getPlaceShipStartPoint(string message) => new(new Point(1, 1), Direction.Horizontal);
            void printOceanGrid(string playerName, OceanGrid oceanGrid) { };
            void printTargetingGrid(string playerName, TargetingGrid targetingGrid) { };
            void printErrorMessage(string message) { };
        }

        [Fact]
        public void ShouldThrowWhenPrintTargetingGridIsNull()
        {
            // arrange
            var playerName = "humanPlayer";

            // act
            Action action = () => _ = new HumanPlayer(
                playerName,
                getPlaceShipStartPoint,
                printOceanGrid,
                callOutPointOnTargetingGrid,
                null,
                printErrorMessage);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();

            StartPoint getPlaceShipStartPoint(string message) => new(new Point(1, 1), Direction.Horizontal);
            void printOceanGrid(string playerName, OceanGrid oceanGrid) { };
            Point callOutPointOnTargetingGrid(string message) => new(1, 1);
            void printErrorMessage(string message) { };
        }

        [Fact]
        public void ShouldThrowWhenPrintErrorMessageIsNull()
        {
            // arrange
            var playerName = "humanPlayer";

            // act
            Action action = () => _ = new HumanPlayer(
                playerName,
                getPlaceShipStartPoint,
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printTargetingGrid,
                null);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();

            StartPoint getPlaceShipStartPoint(string message) => new(new Point(1, 1), Direction.Horizontal);
            void printOceanGrid(string playerName, OceanGrid oceanGrid) { };
            Point callOutPointOnTargetingGrid(string message) => new(1, 1);
            void printTargetingGrid(string playerName, TargetingGrid targetingGrid) { };
        }

        [Theory]
        [InlineData(2, Reply.Hit, true, false)]
        [InlineData(0, Reply.Miss, false, true)]
        [InlineData(2, Reply.Sunk, true, false)]
        public void ShouldSetDefenderAnswer(int defenderAnswershipLength, Reply defenderAnswersReply, bool expectedHit, bool expectedMiss)
        {
            // arrange
            var rule = new FakeOneShipGameRule();
            var attackerPoint = callOutPointOnTargetingGrid("message");
            var startPoint = getPlaceShipStartPoint("message");

            var attacker = new HumanPlayer(
                "humanPlayer",
                getPlaceShipStartPoint,
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printTargetingGrid,
                printErrorMessage);
            attacker.ApplyGameRule(rule);
            attacker.PlaceShipsOnOceanGrid(CancellationToken.None);
            var defenderAnswer = new Answer(defenderAnswershipLength, defenderAnswersReply);

            // act
            attacker.SetDefenderAnswer(attackerPoint, defenderAnswer);
            attacker.PrintOceanGrid();
            attacker.PrintTargetingGrind();

            // assert
            void printTargetingGrid(string playerName, TargetingGrid targetingGrid)
            {
                targetingGrid.TargetingPoints[attackerPoint.Column, attackerPoint.Row].Hit().Should().Be(expectedHit);
                targetingGrid.TargetingPoints[attackerPoint.Column, attackerPoint.Row].Miss().Should().Be(expectedMiss);
                targetingGrid.TargetingPoints[attackerPoint.Column, attackerPoint.Row].DisplayShipLength().Should().Be(defenderAnswershipLength.ToString());
            };

            void printOceanGrid(string playerName, OceanGrid oceanGrid)
            {
                oceanGrid.OceanPoints[startPoint.Point.Column - 1, startPoint.Point.Row].FillOut().Should().BeFalse();
                oceanGrid.OceanPoints[startPoint.Point.Column, startPoint.Point.Row].FillOut().Should().BeTrue();
                oceanGrid.OceanPoints[startPoint.Point.Column + 1, startPoint.Point.Row].FillOut().Should().BeTrue();
                oceanGrid.OceanPoints[startPoint.Point.Column + 3, startPoint.Point.Row].FillOut().Should().BeFalse();
            };

            StartPoint getPlaceShipStartPoint(string message) => new(new Point(2, 2), Direction.Horizontal);
            Point callOutPointOnTargetingGrid(string message) => new(4, 4);
            void printErrorMessage(string message) { };
        }

        [Fact]
        public void ShouldSunkAllShips()
        {
            // arrange
            var rule = new FakeOneShipGameRule();
            var defender = new HumanPlayer(
                "humanPlayer",
                getPlaceShipStartPoint,
                printOceanGrid,
                callOutPointOnTargetingGrid,
                printTargetingGrid,
                printErrorMessage);
            defender.ApplyGameRule(rule);
            defender.PlaceShipsOnOceanGrid(CancellationToken.None);

            // act
            var allShipsSunk1 = defender.AllShipsSunk();
            var defenderAnswer1 = defender.AnswerToAttacker(new Point(1, 1));
            var allShipsSunk2 = defender.AllShipsSunk();
            var defenderAnswer2 = defender.AnswerToAttacker(new Point(2, 2));
            var allShipsSunk3 = defender.AllShipsSunk();
            var defenderAnswer3 = defender.AnswerToAttacker(new Point(3, 2));
            var allShipsSunk4 = defender.AllShipsSunk();

            // assert
            allShipsSunk1.Should().BeFalse();
            defenderAnswer1.Reply.Should().Be(Reply.Miss);
            allShipsSunk2.Should().BeFalse();
            defenderAnswer2.Reply.Should().Be(Reply.Hit);
            allShipsSunk3.Should().BeFalse();
            defenderAnswer3.Reply.Should().Be(Reply.Sunk);
            allShipsSunk4.Should().BeTrue();

            void printTargetingGrid(string playerName, TargetingGrid targetingGrid) { };
            StartPoint getPlaceShipStartPoint(string message) => new(new Point(2, 2), Direction.Horizontal);
            void printOceanGrid(string playerName, OceanGrid oceanGrid) { };
            Point callOutPointOnTargetingGrid(string message) => new(4, 4);
            void printErrorMessage(string message) { };
        }
    }
}
