using Battleships.Domain;
using Battleships.Domain.Ships;

namespace UnitTests.Fakes;

public class FakeTwoShipsGameRule : IGameRule
{
    public IReadOnlyCollection<Ship> GetAllowedShips()
        => Array.AsReadOnly(
            new[]
            {
                FakeShipFactory.CreateBattleship(),
                FakeShipFactory.CreateDestroyer()
            });

    public string GetGameRuleDescription() => "Two ships game rule";
}