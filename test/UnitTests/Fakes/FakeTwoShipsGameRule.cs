namespace UnitTests.Fakes;

using Battleships.Domain;
using Battleships.Domain.Ships;
using System;
using System.Collections.Generic;

public class FakeTwoShipsGameRule : IGameRule
{
    public IReadOnlyCollection<Ship> GetAllowedShips()
        => Array.AsReadOnly(
            new Ship[]
            {
                FakeShipFactory.CreateBattleship(),
                FakeShipFactory.CreateDestroyer()
            });

    public string GetGameRuleDescription() => "Two ships game rule";
}