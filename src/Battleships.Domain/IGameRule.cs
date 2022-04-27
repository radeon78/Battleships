namespace Battleships.Domain;

using Battleships.Domain.Ships;
using System.Collections.Generic;

public interface IGameRule
{
    public IReadOnlyCollection<Ship> GetAllowedShips();

    public string GetGameRuleDescription();
}