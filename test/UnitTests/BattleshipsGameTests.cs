namespace UnitTests
{
    using Battleships.Domain;
    using Battleships.Domain.Grids;
    using Battleships.Domain.Players;
    using FluentAssertions;
    using Moq;
    using System;
    using System.Threading;
    using UnitTests.Fakes;
    using Xunit;

    public class BattleshipsGameTests
    {
        [Fact]
        public void ShouldThrowWhenPlayRuleIsNull()
        {
            // act
            Action action = () => _ = new BattleshipsGame(null, (message) => { });

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ShouldThrowWhenPrintMessageIsNull()
        {
            // act
            Action action = () => _ = new BattleshipsGame(new FakeOneShipGameRule(), null);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ShouldNotThrow()
        {
            // act
            Action action = () => _ = new BattleshipsGame(new FakeOneShipGameRule(), (message) => { });

            // assert
            action.Should().NotThrow();
        }

        [Fact]
        public void ShouldWinPlayer1()
        {
            // arrange
            var token = CancellationToken.None;

            var firstPlayerMock = new Mock<IPlayer>();
            firstPlayerMock.Setup(x => x.ApplyGameRule(It.IsAny<IGameRule>()));
            firstPlayerMock.Setup(x => x.PlaceShipsOnOceanGrid(token));
            firstPlayerMock.Setup(x => x.PrintOceanGrid());
            firstPlayerMock.Setup(x => x.PrintTargetingGrind());
            firstPlayerMock.Setup(x => x.CallOutPointOnTargetingGrid()).Returns(new Point(1, 1));
            firstPlayerMock.SetupSequence(x => x.AnswerToAttacker(It.IsAny<Point>())).Returns(new Answer(2, Reply.Hit)).Returns(new Answer(2, Reply.Sunk));
            firstPlayerMock.Setup(x => x.SetDefenderAnswer(It.IsAny<Point>(), It.IsAny<Answer>()));
            firstPlayerMock.SetupSequence(x => x.AllShipsSunk()).Returns(false).Returns(true);
            firstPlayerMock.Setup(x => x.PlayerName).Returns("player1");

            var secondPlayerMock = new Mock<IPlayer>();
            secondPlayerMock.Setup(x => x.ApplyGameRule(It.IsAny<IGameRule>()));
            secondPlayerMock.Setup(x => x.PlaceShipsOnOceanGrid(token));
            secondPlayerMock.Setup(x => x.PrintOceanGrid());
            secondPlayerMock.Setup(x => x.PrintTargetingGrind());
            secondPlayerMock.Setup(x => x.CallOutPointOnTargetingGrid()).Returns(new Point(1, 1));
            secondPlayerMock.SetupSequence(x => x.AnswerToAttacker(It.IsAny<Point>())).Returns(new Answer(2, Reply.Hit)).Returns(new Answer(2, Reply.Sunk));
            secondPlayerMock.Setup(x => x.SetDefenderAnswer(It.IsAny<Point>(), It.IsAny<Answer>()));
            secondPlayerMock.SetupSequence(x => x.AllShipsSunk()).Returns(false).Returns(true);
            secondPlayerMock.Setup(x => x.PlayerName).Returns("player2");

            var printMessageNumberCalls = 0;
            var game = new BattleshipsGame(new FakeOneShipGameRule(), PrintMessage);

            // act
            game.Start(firstPlayerMock.Object, secondPlayerMock.Object, token);

            // assert
            printMessageNumberCalls.Should().Be(11);

            void PrintMessage(string message)
            {
                printMessageNumberCalls++;

                if (printMessageNumberCalls == 11)
                    message.Should().Be("Game ended. player1 won!");
            }

            firstPlayerMock.Verify(x => x.PrintTargetingGrind(), Times.Exactly(2));
            secondPlayerMock.Verify(x => x.PrintTargetingGrind(), Times.Once);
            firstPlayerMock.Verify(x => x.CallOutPointOnTargetingGrid(), Times.Exactly(2));
            secondPlayerMock.Verify(x => x.CallOutPointOnTargetingGrid(), Times.Once);
        }
    }
}
