namespace Battleships.Domain.PlayRules
{
    using Battleships.Domain.Ships;
    using System.Collections.Generic;

    public interface IPlayRule
    {
        public IReadOnlyCollection<Ship> GetAllowedShips();

        public string GetPlayRuleDescription();
    }
}
