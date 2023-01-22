using Battleships.Domain;
using Battleships.Domain.Ships;

namespace Battleships.UI;

public class ThreeShipsGameRule : IGameRule
{
    public IReadOnlyCollection<Ship> GetAllowedShips()
        => Array.AsReadOnly(
            new[]
            {
                CreateCarrier(),
                CreateBattleship(),
                CreateDestroyer()
            });
    public string GetGameRuleDescription()
        => @"Secretly place your fleet of three on your ocean grid. Your opponent does the same.
Rules for placing ships:
  - Place each ship in any horizontal or vertical position. For each ship you will be asked to type a starting point and position then application calculate and select the rest points on ocean grid
  - Do not place a ship so that any part of it overlaps letters, numbers, the edge of the grid or another ship
  - If you are the first player to sink your opponent's entire fleet of three ships, you win the game!";

    private static Ship CreateCarrier() => new(5);

    private static Ship CreateBattleship() => new(4);

    private static Ship CreateDestroyer() => new(2);
}