namespace UnitTests.Fakes
{
    using Battleships.Domain.PlayRules;
    using Battleships.Domain.Ships;
    using System;
    using System.Collections.Generic;

    public class FakeOneShipsPlayRule : IPlayRule
    {
        public IReadOnlyCollection<Ship> GetAllowedShips()
            => Array.AsReadOnly(
                new Ship[]
                {
                    new Ship(2)
                });
    }
}
