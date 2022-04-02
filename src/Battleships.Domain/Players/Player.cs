namespace Battleships.Domain.Players
{
    using Battleships.Domain.Grids;
    using Battleships.Domain.PlayRules;
    using Battleships.Domain.Ships;
    using System;
    using System.Collections.Generic;

    public abstract class Player : IPlayer
    {
        protected readonly string _playerName;
        protected OceanGrid _oceanGrid;
        protected TargetingGrid _targetingGrid;
        protected IReadOnlyCollection<Ship> _allowedShips;

        public Player(string playerName)
        {
            _playerName = playerName;
            _oceanGrid = new OceanGrid();
            _targetingGrid = new TargetingGrid();
            _allowedShips = Array.Empty<Ship>();
        }

        public void ApplyGameRule(IPlayRule playRule) 
            => _allowedShips = playRule.GetAllowedShips();

        public abstract void PlaceShipsOnGrid();
    }
}
