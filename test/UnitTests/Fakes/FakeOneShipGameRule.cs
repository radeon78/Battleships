namespace UnitTests.Fakes
{
    using Battleships.Domain;
    using Battleships.Domain.Ships;
    using System;
    using System.Collections.Generic;

    public class FakeOneShipGameRule : IGameRule
    {
        public IReadOnlyCollection<Ship> GetAllowedShips()
            => Array.AsReadOnly(
                new Ship[]
                {
                    FakeShipFactory.CreateSubmarine()
                });

        public string GetGameRuleDescription() => "One ship game rule";
    }
}
