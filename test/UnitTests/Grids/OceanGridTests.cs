﻿using Battleships.Domain.Grids;
using Battleships.Domain.Players;
using Battleships.Domain.Ships;
using FluentAssertions;
using UnitTests.Fakes;
using Xunit;

namespace UnitTests.Grids;

public class OceanGridTests
{
    [Theory]
    [InlineData(2, 2, ShipPosition.Horizontal, true, 2, 2, ShipPosition.Horizontal, false)]
    [InlineData(2, 2, ShipPosition.Horizontal, true, 2, 4, ShipPosition.Horizontal, true)]
    [InlineData(0, 0, ShipPosition.Horizontal, true, 1, 1, ShipPosition.Vertical, true)]
    [InlineData(-1, 0, ShipPosition.Horizontal, false, 1, 1, ShipPosition.Vertical, true)]
    [InlineData(0, -1, ShipPosition.Horizontal, false, 1, 1, ShipPosition.Vertical, true)]
    [InlineData(9, 9, ShipPosition.Horizontal, false, 1, 1, ShipPosition.Vertical, true)]
    [InlineData(9, 9, ShipPosition.Vertical, false, 1, 1, ShipPosition.Horizontal, true)]
    [InlineData(10, 1, ShipPosition.Horizontal, false, 1, 1, ShipPosition.Vertical, true)]
    [InlineData(1, 10, ShipPosition.Horizontal, false, 1, 1, ShipPosition.Vertical, true)]
    [InlineData(4, 3, ShipPosition.Vertical, true, 2, 5, ShipPosition.Horizontal, false)]
    [InlineData(4, 3, ShipPosition.Vertical, true, 9, 1, ShipPosition.Vertical, true)]
    [InlineData(4, 3, ShipPosition.Vertical, true, 10, 1, ShipPosition.Vertical, false)]
    public void TryPlaceShip(
        int battleshipColumn, int battleshipRow, ShipPosition battleshipShipPosition, bool battleshipExpectedSuccessResult,
        int destroyerColumn, int destroyerRow, ShipPosition destroyerShipPosition, bool destroyerExpectedSuccessResult)
    {
        // arrange
        var oceanGrid = new OceanGrid();

        var battleship = FakeShipFactory.CreateBattleship();
        var battleshipStartPoint = new StartPoint(new Point(battleshipColumn, battleshipRow), battleshipShipPosition);
        var destroyer = FakeShipFactory.CreateDestroyer();
        var destroyerStartPoint = new StartPoint(new Point(destroyerColumn, destroyerRow), destroyerShipPosition);

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
        var battleshipStartPoint = new StartPoint(new Point(2, 6), ShipPosition.Horizontal);

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
}