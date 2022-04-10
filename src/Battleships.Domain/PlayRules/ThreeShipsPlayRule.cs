namespace Battleships.Domain.PlayRules
{
    using Battleships.Domain.Resources;
    using Battleships.Domain.Ships;
    using System;
    using System.Collections.Generic;

    public class ThreeShipsPlayRule : IPlayRule
    {
        public IReadOnlyCollection<Ship> GetAllowedShips()
            => Array.AsReadOnly(
                new Ship[]
                {
                    Ship.CreateBattleship(),
                    Ship.CreateDestroyer(),
                    Ship.CreateDestroyer()
                });
        public string GetPlayRuleDescription() 
            => string.Format(Resource.PlayRuleDescription, GetAllowedShips().Count);
    }
}
