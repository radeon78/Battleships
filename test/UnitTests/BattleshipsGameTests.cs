namespace UnitTests
{
    using Battleships.Domain;
    using Battleships.Domain.Players;
    using FluentAssertions;
    using Moq;
    using System;
    using System.Threading;
    using UnitTests.Fakes;
    using Xunit;

    public class BattleshipsGameTests
    {
        private readonly Mock<IPlayer> _firstPlayerMock;
        private readonly Mock<IPlayer> _secondPlayerMock;

        public BattleshipsGameTests()
        {
            _firstPlayerMock = new Mock<IPlayer>();
            _secondPlayerMock = new Mock<IPlayer>();
        }

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
            Action action = () => _ = new BattleshipsGame(new FakeOneShipPlayRule(), null);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void ShouldNotThrow()
        {
            // act
            Action action = () => _ = new BattleshipsGame(new FakeOneShipPlayRule(), (message) => { });

            // assert
            action.Should().NotThrow();
        }
    }
}
