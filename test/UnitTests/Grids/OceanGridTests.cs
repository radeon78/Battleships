namespace UnitTests.Grids
{
    using Battleships.Domain.Grids;
    using Battleships.Domain.Players;
    using Battleships.Domain.Ships;
    using FluentAssertions;
    using UnitTests.Fakes;
    using Xunit;

    public class OceanGridTests
    {
        [Theory]
        [InlineData(2, 2, Position.Horizontal, true, 2, 2, Position.Horizontal, false)]
        [InlineData(2, 2, Position.Horizontal, true, 2, 4, Position.Horizontal, true)]
        [InlineData(0, 0, Position.Horizontal, true, 1, 1, Position.Vertical, true)]
        [InlineData(-1, 0, Position.Horizontal, false, 1, 1, Position.Vertical, true)]
        [InlineData(0, -1, Position.Horizontal, false, 1, 1, Position.Vertical, true)]
        [InlineData(9, 9, Position.Horizontal, false, 1, 1, Position.Vertical, true)]
        [InlineData(9, 9, Position.Vertical, false, 1, 1, Position.Horizontal, true)]
        [InlineData(10, 1, Position.Horizontal, false, 1, 1, Position.Vertical, true)]
        [InlineData(1, 10, Position.Horizontal, false, 1, 1, Position.Vertical, true)]
        [InlineData(4, 3, Position.Vertical, true, 2, 5, Position.Horizontal, false)]
        [InlineData(4, 3, Position.Vertical, true, 9, 1, Position.Vertical, true)]
        [InlineData(4, 3, Position.Vertical, true, 10, 1, Position.Vertical, false)]
        public void TryPlaceShip(
            int battleshipColumn, int battleshipRow, Position battleshipPosition, bool battleshipExpectedSuccessResult,
            int destroyerColumn, int destroyerRow, Position destroyerPosition, bool destroyerExpectedSuccessResult)
        {
            // arrange
            var oceanGrid = new OceanGrid();

            var battleship = FakeShipFactory.CreateBattleship();
            var battleshipStartPoint = new StartPoint(new Point(battleshipColumn, battleshipRow), battleshipPosition);
            var destroyer = FakeShipFactory.CreateDestroyer();
            var destroyerStartPoint = new StartPoint(new Point(destroyerColumn, destroyerRow), destroyerPosition);

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
            var battleship = FakeShipFactory.CreateBattleship();
            var battleshipStartPoint = new StartPoint(new Point(2, 6), Position.Horizontal);

            var playRuleWithOneShip = new[] { battleship.Length };
            var playRuleWithTwoTheSameShips = new[] { battleship.Length, battleship.Length };
            var playRuleWithTwoDifferentShips = new[] { battleship.Length, 2 };

            // act
            var placeShipResult = oceanGrid.TryPlaceShip(battleshipStartPoint, battleship);

            var tryHitForMissPointResult1 = oceanGrid.TryHit(missPoint1);
            var tryHitForMissPointResult2 = oceanGrid.TryHit(missPoint2);

            var tryHitForHitPointResult1 = oceanGrid.TryHit(hitPoint1);
            var tryHitForHitPointResult2 = oceanGrid.TryHit(hitPoint2);
            var tryHitForHitPointResult3 = oceanGrid.TryHit(hitPoint3);
            var tryHitForHitPointResult4 = oceanGrid.TryHit(hitPoint4);
            var isAllShipsSunkBeforeLastHit = oceanGrid.AllShipsSunk(playRuleWithOneShip);
            var tryHitForHitPointResult5 = oceanGrid.TryHit(hitPoint5);
            var isAllShipsSunkWhenLastHit = oceanGrid.AllShipsSunk(playRuleWithOneShip);
            var isAllShipsSunkForTwoTheSameShips = oceanGrid.AllShipsSunk(playRuleWithTwoTheSameShips);
            var isAllShipsSunkForTwoDifferentShips = oceanGrid.AllShipsSunk(playRuleWithTwoDifferentShips);

            // assert
            placeShipResult.Should().NotBeNull();
            placeShipResult.IsSuccess.Should().BeTrue();

            tryHitForMissPointResult1.Should().NotBeNull();
            tryHitForMissPointResult1.Reply.Should().Be(Reply.Miss);
            tryHitForMissPointResult1.ShipLength.Should().Be(0);

            tryHitForMissPointResult2.Should().NotBeNull();
            tryHitForMissPointResult2.Reply.Should().Be(Reply.Miss);
            tryHitForMissPointResult2.ShipLength.Should().Be(0);

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

            isAllShipsSunkBeforeLastHit.Should().BeFalse();

            tryHitForHitPointResult5.Should().NotBeNull();
            tryHitForHitPointResult5.Reply.Should().Be(Reply.Sunk);
            tryHitForHitPointResult5.ShipLength.Should().Be(battleship.Length);

            isAllShipsSunkWhenLastHit.Should().BeTrue();
            isAllShipsSunkForTwoTheSameShips.Should().BeFalse();
            isAllShipsSunkForTwoDifferentShips.Should().BeFalse();
        }

        [Fact]
        public void ShouldCloneOceanGrid()
        {
            // arrange
            var oceanGrid = new OceanGrid();
            oceanGrid.TryPlaceShip(new StartPoint(new Point(2, 2), Position.Horizontal), new Ship(1));
            oceanGrid.TryHit(new Point(2, 2));

            var clonedOceanGrid = new OceanGrid(oceanGrid);

            // act
            var result = oceanGrid.Equals(clonedOceanGrid);

            // assert
            result.Should().BeTrue();
            oceanGrid.GetHashCode().Should().Be(clonedOceanGrid.GetHashCode());
        }

        [Fact]
        public void ShouldCompareTheSameOceanGrids()
        {
            // arrange
            var oceanGrid1 = new OceanGrid();
            oceanGrid1.TryPlaceShip(new StartPoint(new Point(2, 2), Position.Horizontal), new Ship(2));
            var oceanGrid2 = new OceanGrid();
            oceanGrid2.TryPlaceShip(new StartPoint(new Point(2, 2), Position.Horizontal), new Ship(2));

            // act
            var result = oceanGrid1.Equals(oceanGrid2);

            // assert
            result.Should().BeTrue();
            oceanGrid1.GetHashCode().Should().Be(oceanGrid2.GetHashCode());
        }

        [Fact]
        public void ShouldCompareDifferentOceanGridsWithDifferentPlaceShip()
        {
            // arrange
            var oceanGrid1 = new OceanGrid();
            oceanGrid1.TryPlaceShip(new StartPoint(new Point(2, 2), Position.Horizontal), new Ship(2));
            var oceanGrid2 = new OceanGrid();
            oceanGrid2.TryPlaceShip(new StartPoint(new Point(3, 2), Position.Horizontal), new Ship(2));

            // act
            var result = oceanGrid1.Equals(oceanGrid2);

            // assert
            result.Should().BeFalse();
            oceanGrid1.GetHashCode().Should().NotBe(oceanGrid2.GetHashCode());
        }

        [Fact]
        public void ShouldCompareDifferentOceanGridsWithDifferentSunkShip()
        {
            // arrange
            var oceanGrid1 = new OceanGrid();
            oceanGrid1.TryPlaceShip(new StartPoint(new Point(2, 2), Position.Horizontal), new Ship(1));
            oceanGrid1.TryHit(new Point(2, 2));

            var oceanGrid2 = new OceanGrid();
            oceanGrid2.TryPlaceShip(new StartPoint(new Point(2, 2), Position.Horizontal), new Ship(1));

            // act
            var result = oceanGrid1.Equals(oceanGrid2);

            // assert
            result.Should().BeFalse();
            oceanGrid1.GetHashCode().Should().NotBe(oceanGrid2.GetHashCode());
        }

        [Fact]
        public void ShouldCompareOceanGridWithNull()
        {
            // arrange
            var oceanGrid1 = new OceanGrid();

            // act
            var result = oceanGrid1.Equals(null);

            // assert
            result.Should().BeFalse();
        }
    }
}
