namespace UnitTests.Fakes
{
    using Battleships.Domain.PlayRules;
    using Battleships.Domain.Ships;
    using System;
    using System.Collections.Generic;

    public class FakeTwoShipsPlayRule : IPlayRule
    {
        public IReadOnlyCollection<Ship> GetAllowedShips()
            => Array.AsReadOnly(
                new Ship[]
                {
                    Ship.CreateBattleship(),
                    Ship.CreateDestroyer()
                });
    }
}
