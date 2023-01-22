using Battleships.Domain;
using Battleships.Domain.Ships;

namespace UnitTests.Fakes;

public class FakeOneShipGameRule : IGameRule
{
    public IReadOnlyCollection<Ship> GetAllowedShips()
        => Array.AsReadOnly(
            new[]
            {
                FakeShipFactory.CreateSubmarine()
            });

    public string GetGameRuleDescription() => "One ship game rule";
}