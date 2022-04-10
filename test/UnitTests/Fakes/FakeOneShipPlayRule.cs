namespace UnitTests.Fakes
{
    using Battleships.Domain.PlayRules;
    using Battleships.Domain.Resources;
    using Battleships.Domain.Ships;
    using System;
    using System.Collections.Generic;

    public class FakeOneShipPlayRule : IPlayRule
    {
        public IReadOnlyCollection<Ship> GetAllowedShips()
            => Array.AsReadOnly(
                new Ship[]
                {
                    new Ship(2)
                });
        public string GetPlayRuleDescription() 
            => string.Format(Resource.PlayRuleDescription, GetAllowedShips().Count);
    }
}
