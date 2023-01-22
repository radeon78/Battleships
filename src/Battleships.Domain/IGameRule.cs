using Battleships.Domain.Ships;

namespace Battleships.Domain;

public interface IGameRule
{
    public IReadOnlyCollection<Ship> GetAllowedShips();

    public string GetGameRuleDescription();
}