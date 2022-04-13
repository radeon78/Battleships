namespace UnitTests.Fakes
{
    using Battleships.Domain;
    using Battleships.Domain.Ships;
    using System;
    using System.Collections.Generic;

    public class FakeThreeShipsGameRule : IGameRule
    {
        public IReadOnlyCollection<Ship> GetAllowedShips()
            => Array.AsReadOnly(
                new Ship[]
                {
                    FakeShipFactory.CreateBattleship(),
                    FakeShipFactory.CreateDestroyer(),
                    FakeShipFactory.CreateDestroyer()
                });
        public string GetGameRuleDescription() => "Three ships game rule";
    }
}
