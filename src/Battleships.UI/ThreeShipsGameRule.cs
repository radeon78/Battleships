namespace Battleships.UI
{
    using Battleships.Domain;
    using Battleships.Domain.Ships;
    using System;
    using System.Collections.Generic;

    public class ThreeShipsGameRule : IGameRule
    {
        public IReadOnlyCollection<Ship> GetAllowedShips()
            => Array.AsReadOnly(
                new Ship[]
                {
                    CreateBattleship(),
                    CreateDestroyer(),
                    CreateDestroyer()
                });
        public string GetGameRuleDescription()
            => @"Secretly place your fleet of three on your ocean grid. Your opponent does the same.
Rules for placing ships:
  - Place each ship in any horizontal or vertical position, but not diagonally
  - Do not place a ship so that any part of it overlaps letters, numbers, the edge of the grid or another ship
  - If you are the first player to sink your opponent's entire fleet of three ships, you win the game!";

        private static Ship CreateBattleship() => new(5);

        private static Ship CreateDestroyer() => new(4);
    }
}
