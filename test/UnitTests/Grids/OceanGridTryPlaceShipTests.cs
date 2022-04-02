namespace UnitTests.Grids
{
    using Battleships.Domain.Grids;
    using Battleships.Domain.Ships;
    using FluentAssertions;
    using Xunit;

    public class OceanGridTryPlaceShipTests
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
    }
}
