namespace UnitTests.Grids
{
    using Battleships.Domain.Grids;
    using Battleships.Domain.Players;
    using Battleships.Domain.Ships;
    using FluentAssertions;
    using System.Collections.Generic;
    using Xunit;

    public class OceanGridTests
    {
        [Theory]
        [InlineData(2, 2, Direction.Horizontal, true, 2, 2, Direction.Horizontal, false)]
        [InlineData(2, 2, Direction.Horizontal, true, 2, 4, Direction.Horizontal, true)]
        [InlineData(0, 0, Direction.Horizontal, true, 1, 1, Direction.Vertical, false)]
        [InlineData(-1, 0, Direction.Horizontal, false, 1, 1, Direction.Vertical, true)]
        [InlineData(0, -1, Direction.Horizontal, false, 1, 1, Direction.Vertical, true)]
        [InlineData(9, 9, Direction.Horizontal, false, 1, 1, Direction.Vertical, true)]
        [InlineData(9, 9, Direction.Vertical, false, 1, 1, Direction.Horizontal, true)]
        [InlineData(10, 1, Direction.Horizontal, false, 1, 1, Direction.Vertical, true)]
        [InlineData(1, 10, Direction.Horizontal, false, 1, 1, Direction.Vertical, true)]
        [InlineData(4, 3, Direction.Vertical, true, 2, 5, Direction.Horizontal, false)]
        [InlineData(4, 3, Direction.Vertical, true, 9, 1, Direction.Vertical, true)]
        [InlineData(4, 3, Direction.Vertical, true, 10, 1, Direction.Vertical, false)]
        public void TryPlaceShip(
            int battleshipColumn, int battleshipRow, Direction battleshipDirection, bool battleshipExpectedSuccessResult,
            int destroyerColumn, int destroyerRow, Direction destroyerDirection, bool destroyerExpectedSuccessResult)
        {
            // arrange
            var oceanGrid = new OceanGrid();

            var battleship = Ship.CreateBattleship();
            var battleshipStartPoint = new StartPoint(new Point(battleshipColumn, battleshipRow), battleshipDirection);
            var destroyer = Ship.CreateDestroyer();
            var destroyerStartPoint = new StartPoint(new Point(destroyerColumn, destroyerRow), destroyerDirection);

            // act
            var battleshipResult = oceanGrid.TryPlaceShip(battleshipStartPoint, battleship);
            var destroyer1Result = oceanGrid.TryPlaceShip(destroyerStartPoint, destroyer);

            // assert
            battleshipResult.IsSuccess.Should().Be(battleshipExpectedSuccessResult);
            destroyer1Result.IsSuccess.Should().Be(destroyerExpectedSuccessResult);
        }

        [Fact]
        public void ShouldMissShip()
        {
            // arrange
            var oceanGrid = new OceanGrid();
            var point = new Point(1, 3);

            // act
            var result = oceanGrid.TryHit(point);

            // assert
            result.Should().NotBeNull();
            result.Reply.Should().Be(Reply.Miss);
            result.ShipLength.Should().Be(0);
        }

        [Fact]
        public void ShouldHitAndThenSunkShip()
        {
            // arrange
            var missPoint1 = new Point(2, 5);
            var hitPoint1 = new Point(2, 6);
            var hitPoint2 = new Point(3, 6);
            var hitPoint3 = new Point(4, 6);
            var hitPoint4 = new Point(5, 6);
            var hitPoint5 = new Point(6, 6);
            var missPoint2 = new Point(7, 6);

            var oceanGrid = new OceanGrid();
            var battleship = Ship.CreateBattleship();
            var oneShip = new List<int> { battleship.Length };
            var twoTheSameShips = new List<int> { battleship.Length, battleship.Length };
            var twoDifferentShips = new List<int> { battleship.Length, 2 };
            var battleshipStartPoint = new StartPoint(new Point(2, 6), Direction.Horizontal);

            // act
            var placeShipResult = oceanGrid.TryPlaceShip(battleshipStartPoint, battleship);
            var tryHitForMissPointResult1 = oceanGrid.TryHit(missPoint1);
            var tryHitForHitPointResult1 = oceanGrid.TryHit(hitPoint1);
            var tryHitForHitPointResult2 = oceanGrid.TryHit(hitPoint2);
            var tryHitForHitPointResult3 = oceanGrid.TryHit(hitPoint3);
            var tryHitForHitPointResult4 = oceanGrid.TryHit(hitPoint4);
            var allShipsSunkBeforeLastHit = oceanGrid.AllShipsSunk(oneShip);
            var tryHitForHitPointResult5 = oceanGrid.TryHit(hitPoint5);
            var allShipsSunkWhenLastHit = oceanGrid.AllShipsSunk(oneShip);
            var allShipsSunkForTwoTheSameShips = oceanGrid.AllShipsSunk(twoTheSameShips);
            var allShipsSunkForTwoDifferentShips = oceanGrid.AllShipsSunk(twoDifferentShips);
            var tryHitForMissPointResult2 = oceanGrid.TryHit(missPoint2);

            // assert
            placeShipResult.Should().NotBeNull();
            placeShipResult.IsSuccess.Should().BeTrue();

            tryHitForMissPointResult1.Should().NotBeNull();
            tryHitForMissPointResult1.Reply.Should().Be(Reply.Miss);
            tryHitForMissPointResult1.ShipLength.Should().Be(0);

            tryHitForHitPointResult1.Should().NotBeNull();
            tryHitForHitPointResult1.Reply.Should().Be(Reply.Hit);
            tryHitForHitPointResult1.ShipLength.Should().Be(battleship.Length);

            tryHitForHitPointResult2.Should().NotBeNull();
            tryHitForHitPointResult2.Reply.Should().Be(Reply.Hit);
            tryHitForHitPointResult2.ShipLength.Should().Be(battleship.Length);

            tryHitForHitPointResult3.Should().NotBeNull();
            tryHitForHitPointResult3.Reply.Should().Be(Reply.Hit);
            tryHitForHitPointResult3.ShipLength.Should().Be(battleship.Length);

            tryHitForHitPointResult4.Should().NotBeNull();
            tryHitForHitPointResult4.Reply.Should().Be(Reply.Hit);
            tryHitForHitPointResult4.ShipLength.Should().Be(battleship.Length);

            allShipsSunkBeforeLastHit.Should().BeFalse();

            tryHitForHitPointResult5.Should().NotBeNull();
            tryHitForHitPointResult5.Reply.Should().Be(Reply.Sunk);
            tryHitForHitPointResult5.ShipLength.Should().Be(battleship.Length);

            allShipsSunkWhenLastHit.Should().BeTrue();
            allShipsSunkForTwoTheSameShips.Should().BeFalse();
            allShipsSunkForTwoDifferentShips.Should().BeFalse();

            tryHitForMissPointResult2.Should().NotBeNull();
            tryHitForMissPointResult2.Reply.Should().Be(Reply.Miss);
            tryHitForMissPointResult2.ShipLength.Should().Be(0);
        }
    }
}
