namespace UnitTests.Players
{
    using Battleships.Domain.Ships;
    using Moq;
    using Battleships.Domain.Grids;
    using Battleships.Domain.Players;
    using FluentAssertions;
    using System;
    using System.Threading;
    using UnitTests.Fakes;
    using Xunit;

    public class HumanPlayerTests
    {
        private readonly Mock<Func<string, StartPoint>> _getPlaceShipStartPointMock;
        private readonly Mock<Action<string, OceanGrid>> _printOceanGridMock;
        private readonly Mock<Func<string, Point>> _callOutPointOnTargetingGridMock;
        private readonly Mock<Action<string, TargetingGrid>> _printTargetingGridMock;
        private readonly Mock<Action<string>> _printErrorMessageMock;

        public HumanPlayerTests()
        {
            _getPlaceShipStartPointMock = new Mock<Func<string, StartPoint>>();
            _printOceanGridMock = new Mock<Action<string, OceanGrid>>();
            _callOutPointOnTargetingGridMock = new Mock<Func<string, Point>>();
            _printTargetingGridMock = new Mock<Action<string, TargetingGrid>>();
            _printErrorMessageMock = new Mock<Action<string>>();
        }

        [Fact]
        public void ShouldApplyGameRule()
        {
            // arrange
            _getPlaceShipStartPointMock.Setup(x => x(It.IsAny<string>())).Returns(new StartPoint(new Point(1, 1), ShipPosition.Horizontal));
            _printOceanGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()));
            _callOutPointOnTargetingGridMock.Setup(x => x(It.IsAny<string>())).Returns(new Point(1, 1));
            _printTargetingGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()));
            _printErrorMessageMock.Setup(x => x(It.IsAny<string>()));
            var playerName = "humanPlayer";

            var rule = new FakeThreeShipsGameRule();
            var player = new HumanPlayer(
                playerName,
                _getPlaceShipStartPointMock.Object,
                _printOceanGridMock.Object,
                _callOutPointOnTargetingGridMock.Object,
                _printTargetingGridMock.Object,
                _printErrorMessageMock.Object);

            // act
            Action action = () => player.ApplyGameRule(rule);

            // assert
            action.Should().NotThrow();
            _getPlaceShipStartPointMock.Verify(x => x(It.IsAny<string>()), Times.Never);
            _printOceanGridMock.Verify(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()), Times.Never);
            _callOutPointOnTargetingGridMock.Verify(x => x(It.IsAny<string>()), Times.Never);
            _printTargetingGridMock.Verify(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()), Times.Never);
            _printErrorMessageMock.Verify(x => x(It.IsAny<string>()), Times.Never);
            player.PlayerName.Should().Be(playerName);
        }

        [Fact]
        public void ShouldPlaceShipsOnGridWithNoError()
        {
            // arrange
            _getPlaceShipStartPointMock
                .SetupSequence(x => x(It.IsAny<string>()))
                .Returns(new StartPoint(new Point(0, 0), ShipPosition.Horizontal))
                .Returns(new StartPoint(new Point(2, 2), ShipPosition.Horizontal))
                .Returns(new StartPoint(new Point(5, 5), ShipPosition.Vertical));
            _printOceanGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()));
            _callOutPointOnTargetingGridMock.Setup(x => x(It.IsAny<string>())).Returns(new Point(2, 2));
            _printTargetingGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()));
            _printErrorMessageMock.Setup(x => x(It.IsAny<string>()));

            var rule = new FakeThreeShipsGameRule();
            var player = new HumanPlayer(
                "humanPlayer",
                _getPlaceShipStartPointMock.Object,
                _printOceanGridMock.Object,
                _callOutPointOnTargetingGridMock.Object,
                _printTargetingGridMock.Object,
                _printErrorMessageMock.Object);
            player.ApplyGameRule(rule);

            // act
            Action action = () => player.PlaceShipsOnOceanGrid(CancellationToken.None);

            // assert
            action.Should().NotThrow();
            _getPlaceShipStartPointMock.Verify(x => x(It.IsAny<string>()), Times.Exactly(3));
            _printOceanGridMock.Verify(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()), Times.Exactly(3));
            _callOutPointOnTargetingGridMock.Verify(x => x(It.IsAny<string>()), Times.Never);
            _printTargetingGridMock.Verify(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()), Times.Never);
            _printErrorMessageMock.Verify(x => x(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public void ShouldPlaceShipsOnGridWithTwoErrors()
        {
            // arrange
            _getPlaceShipStartPointMock
                .SetupSequence(x => x(It.IsAny<string>()))
                .Returns(new StartPoint(new Point(1, 1), ShipPosition.Horizontal))
                .Returns(new StartPoint(new Point(2, 1), ShipPosition.Vertical))
                .Returns(new StartPoint(new Point(5, 1), ShipPosition.Vertical))
                .Returns(new StartPoint(new Point(5, 5), ShipPosition.Vertical))
                .Returns(new StartPoint(new Point(6, 5), ShipPosition.Horizontal));
            _printOceanGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()));
            _callOutPointOnTargetingGridMock.Setup(x => x(It.IsAny<string>())).Returns(new Point(2, 2));
            _printTargetingGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()));
            _printErrorMessageMock.Setup(x => x(It.IsAny<string>()));

            var rule = new FakeThreeShipsGameRule();
            var player = new HumanPlayer(
                "humanPlayer",
                _getPlaceShipStartPointMock.Object,
                _printOceanGridMock.Object,
                _callOutPointOnTargetingGridMock.Object,
                _printTargetingGridMock.Object,
                _printErrorMessageMock.Object);
            player.ApplyGameRule(rule);

            // act
            Action action = () => player.PlaceShipsOnOceanGrid(CancellationToken.None);

            // assert
            action.Should().NotThrow();
            _getPlaceShipStartPointMock.Verify(x => x(It.IsAny<string>()), Times.Exactly(5));
            _printOceanGridMock.Verify(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()), Times.Exactly(3));
            _callOutPointOnTargetingGridMock.Verify(x => x(It.IsAny<string>()), Times.Never);
            _printTargetingGridMock.Verify(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()), Times.Never);
            _printErrorMessageMock.Verify(x => x(It.IsAny<string>()), Times.Exactly(2));
        }

        [Fact]
        public void ShouldCancelPlaceShipsOnGrid()
        {
            // arrange
            var source = new CancellationTokenSource();
            var token = source.Token;

            _getPlaceShipStartPointMock.Setup(x => x(It.IsAny<string>())).Returns(new StartPoint(new Point(1, 1), ShipPosition.Horizontal));
            _printOceanGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()));
            _callOutPointOnTargetingGridMock.Setup(x => x(It.IsAny<string>())).Returns(new Point(2, 2));
            _printTargetingGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()));
            _printErrorMessageMock.Setup(x => x(It.IsAny<string>()));

            var rule = new FakeThreeShipsGameRule();
            var player = new HumanPlayer(
                "humanPlayer",
                _getPlaceShipStartPointMock.Object,
                _printOceanGridMock.Object,
                _callOutPointOnTargetingGridMock.Object,
                _printTargetingGridMock.Object,
                _printErrorMessageMock.Object);
            player.ApplyGameRule(rule);

            // act
            Action action = () => player.PlaceShipsOnOceanGrid(token);
            source.Cancel();

            // assert
            action.Should().NotThrow();
            _getPlaceShipStartPointMock.Verify(x => x(It.IsAny<string>()), Times.Never);
            _printOceanGridMock.Verify(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()), Times.Never);
            _callOutPointOnTargetingGridMock.Verify(x => x(It.IsAny<string>()), Times.Never);
            _printTargetingGridMock.Verify(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()), Times.Never);
            _printErrorMessageMock.Verify(x => x(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ShouldCancelPlaceShipsOnGridAfterOneCall()
        {
            // arrange
            var source = new CancellationTokenSource();
            var token = source.Token;

            _getPlaceShipStartPointMock
                .Setup(x => x(It.IsAny<string>()))
                .Returns(() =>
                {
                    source!.Cancel();
                    return new StartPoint(new Point(1, 1), ShipPosition.Horizontal);
                });
            _printOceanGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()));
            _callOutPointOnTargetingGridMock.Setup(x => x(It.IsAny<string>())).Returns(new Point(2, 2));
            _printTargetingGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()));
            _printErrorMessageMock.Setup(x => x(It.IsAny<string>()));

            var rule = new FakeThreeShipsGameRule();
            var player = new HumanPlayer(
                "humanPlayer",
                _getPlaceShipStartPointMock.Object,
                _printOceanGridMock.Object,
                _callOutPointOnTargetingGridMock.Object,
                _printTargetingGridMock.Object,
                _printErrorMessageMock.Object);
            player.ApplyGameRule(rule);

            // act
            Action action = () => player.PlaceShipsOnOceanGrid(token);

            // assert
            action.Should().NotThrow();
            _getPlaceShipStartPointMock.Verify(x => x(It.IsAny<string>()), Times.Once);
            _printOceanGridMock.Verify(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()), Times.Never);
            _callOutPointOnTargetingGridMock.Verify(x => x(It.IsAny<string>()), Times.Never);
            _printTargetingGridMock.Verify(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()), Times.Never);
            _printErrorMessageMock.Verify(x => x(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ShouldCallOutPointOnTargetingGridWithNoError()
        {
            // arrange
            _getPlaceShipStartPointMock.Setup(x => x(It.IsAny<string>())).Returns(() => new StartPoint(new Point(1, 1), ShipPosition.Horizontal));
            _printOceanGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()));
            _callOutPointOnTargetingGridMock.Setup(x => x(It.IsAny<string>())).Returns(new Point(4, 2));
            _printTargetingGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()));
            _printErrorMessageMock.Setup(x => x(It.IsAny<string>()));

            var rule = new FakeThreeShipsGameRule();
            var player = new HumanPlayer(
                "humanPlayer",
                _getPlaceShipStartPointMock.Object,
                _printOceanGridMock.Object,
                _callOutPointOnTargetingGridMock.Object,
                _printTargetingGridMock.Object,
                _printErrorMessageMock.Object);
            player.ApplyGameRule(rule);

            // act
            var point = player.CallOutPointOnTargetingGrid();

            // assert
            point.Should().NotBeNull();
            point.Row.Should().Be(2);
            point.Column.Should().Be(4);
            _getPlaceShipStartPointMock.Verify(x => x(It.IsAny<string>()), Times.Never);
            _printOceanGridMock.Verify(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()), Times.Never);
            _callOutPointOnTargetingGridMock.Verify(x => x(It.IsAny<string>()), Times.Once);
            _printTargetingGridMock.Verify(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()), Times.Never);
            _printErrorMessageMock.Verify(x => x(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ShouldCallOutPointOnTargetingGridWithOneError()
        {
            // arrange
            _getPlaceShipStartPointMock.Setup(x => x(It.IsAny<string>())).Returns(() => new StartPoint(new Point(1, 1), ShipPosition.Horizontal));
            _printOceanGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()));
            _callOutPointOnTargetingGridMock
                .SetupSequence(x => x(It.IsAny<string>()))
                .Returns(new Point(10, 0))
                .Returns(new Point(4, 2));
            _printTargetingGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()));
            _printErrorMessageMock.Setup(x => x(It.IsAny<string>()));

            var rule = new FakeThreeShipsGameRule();
            var player = new HumanPlayer(
                "humanPlayer",
                _getPlaceShipStartPointMock.Object,
                _printOceanGridMock.Object,
                _callOutPointOnTargetingGridMock.Object,
                _printTargetingGridMock.Object,
                _printErrorMessageMock.Object);
            player.ApplyGameRule(rule);

            // act
            var point = player.CallOutPointOnTargetingGrid();

            // assert
            point.Should().NotBeNull();
            point.Row.Should().Be(2);
            point.Column.Should().Be(4);
            _getPlaceShipStartPointMock.Verify(x => x(It.IsAny<string>()), Times.Never);
            _printOceanGridMock.Verify(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()), Times.Never);
            _callOutPointOnTargetingGridMock.Verify(x => x(It.IsAny<string>()), Times.Exactly(2));
            _printTargetingGridMock.Verify(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()), Times.Never);
            _printErrorMessageMock.Verify(x => x(It.IsAny<string>()), Times.Once);
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
            _getPlaceShipStartPointMock
                .SetupSequence(x => x(It.IsAny<string>()))
                .Returns(() => new StartPoint(new Point(3, 9), ShipPosition.Horizontal))
                .Returns(() => new StartPoint(new Point(9, 1), ShipPosition.Vertical));
            _printOceanGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()));
            _callOutPointOnTargetingGridMock.Setup(x => x(It.IsAny<string>())).Returns(new Point(2, 2));
            _printTargetingGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()));
            _printErrorMessageMock.Setup(x => x(It.IsAny<string>()));

            var rule = new FakeTwoShipsGameRule();
            var player = new HumanPlayer(
                "humanPlayer",
                _getPlaceShipStartPointMock.Object,
                _printOceanGridMock.Object,
                _callOutPointOnTargetingGridMock.Object,
                _printTargetingGridMock.Object,
                _printErrorMessageMock.Object);
            player.ApplyGameRule(rule);
            player.PlaceShipsOnOceanGrid(CancellationToken.None);

            var point = new Point(column, row);

            // act
            var answer = player.AnswerToAttacker(point);

            // assert
            answer.Should().NotBeNull();
            answer.Reply.Should().Be(expectedReply);
            answer.ShipLength.Should().Be(expectedShipLength);
            _getPlaceShipStartPointMock.Verify(x => x(It.IsAny<string>()), Times.Exactly(2));
            _printOceanGridMock.Verify(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()), Times.Exactly(2));
            _callOutPointOnTargetingGridMock.Verify(x => x(It.IsAny<string>()), Times.Never);
            _printTargetingGridMock.Verify(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()), Times.Never);
            _printErrorMessageMock.Verify(x => x(It.IsAny<string>()), Times.Never);
            rule.GetGameRuleDescription().Should().Be("Two ships game rule");
        }

        [Fact]
        public void ShouldAnswerToAttackerHitAndThenSunk()
        {
            // arrange
            _getPlaceShipStartPointMock.Setup(x => x(It.IsAny<string>())).Returns(() => new StartPoint(new Point(4, 4), ShipPosition.Horizontal));
            _printOceanGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()));
            _callOutPointOnTargetingGridMock.Setup(x => x(It.IsAny<string>())).Returns(new Point(2, 2));
            _printTargetingGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()));
            _printErrorMessageMock.Setup(x => x(It.IsAny<string>()));

            var rule = new FakeOneShipGameRule();
            var player = new HumanPlayer(
                "humanPlayer",
                _getPlaceShipStartPointMock.Object,
                _printOceanGridMock.Object,
                _callOutPointOnTargetingGridMock.Object,
                _printTargetingGridMock.Object,
                _printErrorMessageMock.Object);
            player.ApplyGameRule(rule);
            player.PlaceShipsOnOceanGrid(CancellationToken.None);

            var point1 = new Point(4, 4);
            var point2 = new Point(5, 4);

            // act
            var answer1 = player.AnswerToAttacker(point1);
            var answer2 = player.AnswerToAttacker(point2);

            // assert
            answer1.Should().NotBeNull();
            answer1.Reply.Should().Be(Reply.Hit);
            answer1.ShipLength.Should().Be(2);

            answer2.Should().NotBeNull();
            answer2.Reply.Should().Be(Reply.Sunk);
            answer2.ShipLength.Should().Be(2);

            _getPlaceShipStartPointMock.Verify(x => x(It.IsAny<string>()), Times.Once);
            _printOceanGridMock.Verify(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()), Times.Once);
            _callOutPointOnTargetingGridMock.Verify(x => x(It.IsAny<string>()), Times.Never);
            _printTargetingGridMock.Verify(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()), Times.Never);
            _printErrorMessageMock.Verify(x => x(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ShouldThrowWhenGetPlaceShipStartPointIsNull()
        {
            // arrange
            _printOceanGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()));
            _callOutPointOnTargetingGridMock.Setup(x => x(It.IsAny<string>())).Returns(new Point(2, 2));
            _printTargetingGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()));
            _printErrorMessageMock.Setup(x => x(It.IsAny<string>()));
            var playerName = "humanPlayer";

            // act
            Action action = () => _ = new HumanPlayer(
                playerName,
                null,
                _printOceanGridMock.Object,
                _callOutPointOnTargetingGridMock.Object,
                _printTargetingGridMock.Object,
                _printErrorMessageMock.Object);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ShouldThrowWhenPrintOceanGridIsNull()
        {
            // arrange
            _getPlaceShipStartPointMock.Setup(x => x(It.IsAny<string>())).Returns(() => new StartPoint(new Point(4, 4), ShipPosition.Horizontal));
            _callOutPointOnTargetingGridMock.Setup(x => x(It.IsAny<string>())).Returns(new Point(2, 2));
            _printTargetingGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()));
            _printErrorMessageMock.Setup(x => x(It.IsAny<string>()));
            var playerName = "humanPlayer";

            // act
            Action action = () => _ = new HumanPlayer(
                playerName,
                _getPlaceShipStartPointMock.Object,
                null,
                _callOutPointOnTargetingGridMock.Object,
                _printTargetingGridMock.Object,
                _printErrorMessageMock.Object);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ShouldThrowWhenCallOutPointOnTargetingGridIsNull()
        {
            // arrange
            _getPlaceShipStartPointMock.Setup(x => x(It.IsAny<string>())).Returns(() => new StartPoint(new Point(4, 4), ShipPosition.Horizontal));
            _printOceanGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()));
            _printTargetingGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()));
            _printErrorMessageMock.Setup(x => x(It.IsAny<string>()));
            var playerName = "humanPlayer";

            // act
            Action action = () => _ = new HumanPlayer(
                playerName,
                _getPlaceShipStartPointMock.Object,
                _printOceanGridMock.Object,
                null,
                _printTargetingGridMock.Object,
                _printErrorMessageMock.Object);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ShouldThrowWhenPrintTargetingGridIsNull()
        {
            // arrange
            _getPlaceShipStartPointMock.Setup(x => x(It.IsAny<string>())).Returns(() => new StartPoint(new Point(4, 4), ShipPosition.Horizontal));
            _printOceanGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()));
            _callOutPointOnTargetingGridMock.Setup(x => x(It.IsAny<string>())).Returns(new Point(2, 2));
            _printErrorMessageMock.Setup(x => x(It.IsAny<string>()));
            var playerName = "humanPlayer";

            // act
            Action action = () => _ = new HumanPlayer(
                playerName,
                _getPlaceShipStartPointMock.Object,
                _printOceanGridMock.Object,
                _callOutPointOnTargetingGridMock.Object,
                null,
                _printErrorMessageMock.Object);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ShouldThrowWhenPrintErrorMessageIsNull()
        {
            // arrange
            _getPlaceShipStartPointMock.Setup(x => x(It.IsAny<string>())).Returns(() => new StartPoint(new Point(4, 4), ShipPosition.Horizontal));
            _printOceanGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()));
            _callOutPointOnTargetingGridMock.Setup(x => x(It.IsAny<string>())).Returns(new Point(2, 2));
            _printTargetingGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()));
            var playerName = "humanPlayer";

            // act
            Action action = () => _ = new HumanPlayer(
                playerName,
                _getPlaceShipStartPointMock.Object,
                _printOceanGridMock.Object,
                _callOutPointOnTargetingGridMock.Object,
                _printTargetingGridMock.Object,
                null);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Theory]
        [InlineData(2, Reply.Hit, true, false)]
        [InlineData(0, Reply.Miss, false, true)]
        [InlineData(2, Reply.Sunk, true, false)]
        public void ShouldSetDefenderAnswer(int defenderAnswerShipLength, Reply defenderAnswersReply, bool expectedHit, bool expectedMiss)
        {
            // arrange
            var startPoint = new StartPoint(new Point(2, 2), ShipPosition.Horizontal);
            var attackerPoint = new Point(4, 4);
            _getPlaceShipStartPointMock.Setup(x => x(It.IsAny<string>())).Returns(startPoint);
            _callOutPointOnTargetingGridMock.Setup(x => x(It.IsAny<string>())).Returns(attackerPoint);
            _printErrorMessageMock.Setup(x => x(It.IsAny<string>()));

            var rule = new FakeOneShipGameRule();

            var attacker = new HumanPlayer(
                "humanPlayer",
                _getPlaceShipStartPointMock.Object,
                PrintOceanGrid,
                _callOutPointOnTargetingGridMock.Object,
                PrintTargetingGrid,
                _printErrorMessageMock.Object);
            attacker.ApplyGameRule(rule);
            attacker.PlaceShipsOnOceanGrid(CancellationToken.None);
            var defenderAnswer = new Answer(defenderAnswerShipLength, defenderAnswersReply);

            // act
            attacker.SetDefenderAnswer(attackerPoint, defenderAnswer);
            attacker.PrintOceanGrid();
            attacker.PrintTargetingGrind();

            // assert
            void PrintTargetingGrid(string playerName, TargetingGrid targetingGrid)
            {
                targetingGrid[attackerPoint.Column, attackerPoint.Row].Hit().Should().Be(expectedHit);
                targetingGrid[attackerPoint.Column, attackerPoint.Row].Miss().Should().Be(expectedMiss);
                targetingGrid[attackerPoint.Column, attackerPoint.Row].DisplayShipLength().Should().Be(defenderAnswerShipLength.ToString());
            };

            void PrintOceanGrid(string playerName, OceanGrid oceanGrid)
            {
                oceanGrid[startPoint.Point.Column - 1, startPoint.Point.Row].FillOut().Should().BeFalse();
                oceanGrid[startPoint.Point.Column, startPoint.Point.Row].FillOut().Should().BeTrue();
                oceanGrid[startPoint.Point.Column + 1, startPoint.Point.Row].FillOut().Should().BeTrue();
                oceanGrid[startPoint.Point.Column + 3, startPoint.Point.Row].FillOut().Should().BeFalse();
            };
        }

        [Fact]
        public void ShouldSunkAllShips()
        {
            // arrange
            _getPlaceShipStartPointMock.Setup(x => x(It.IsAny<string>())).Returns(() => new StartPoint(new Point(2, 2), ShipPosition.Horizontal));
            _printOceanGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<OceanGrid>()));
            _callOutPointOnTargetingGridMock.Setup(x => x(It.IsAny<string>())).Returns(new Point(4, 4));
            _printTargetingGridMock.Setup(x => x(It.IsAny<string>(), It.IsAny<TargetingGrid>()));
            _printErrorMessageMock.Setup(x => x(It.IsAny<string>()));

            var rule = new FakeOneShipGameRule();
            var defender = new HumanPlayer(
                "humanPlayer",
                _getPlaceShipStartPointMock.Object,
                _printOceanGridMock.Object,
                _callOutPointOnTargetingGridMock.Object,
                _printTargetingGridMock.Object,
                _printErrorMessageMock.Object);
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
        }
    }
}
