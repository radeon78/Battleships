using Battleships.Domain;
using Battleships.Domain.Ships;

namespace UnitTests.Fakes;

public class FakeThreeShipsGameRule : IGameRule
{
    public IReadOnlyCollection<Ship> GetAllowedShips()
        => Array.AsReadOnly(
            new[]
            {
                FakeShipFactory.CreateBattleship(),
                FakeShipFactory.CreateDestroyer(),
                FakeShipFactory.CreateDestroyer()
            });
    public string GetGameRuleDescription() => "Three ships game rule";
}