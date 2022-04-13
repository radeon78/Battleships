namespace Battleships.Domain.Players
{
    using Battleships.Domain;
    using Battleships.Domain.Grids;
    using Battleships.Domain.Ships;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public abstract class Player : IPlayer
    {
        protected readonly string _playerName;
        protected OceanGrid _oceanGrid;
        protected TargetingGrid _targetingGrid;
        protected IReadOnlyCollection<Ship> _allowedShips;

        public Player(string playerName)
        {
            if (string.IsNullOrEmpty(playerName))
                throw new ArgumentNullException(nameof(playerName));

            _playerName = playerName;
            _oceanGrid = new OceanGrid();
            _targetingGrid = new TargetingGrid();
            _allowedShips = Array.Empty<Ship>();
        }

        public string PlayerName => _playerName;

        public void ApplyGameRule(IGameRule gameRule)
            => _allowedShips = gameRule.GetAllowedShips();

        public abstract void PlaceShipsOnOceanGrid(CancellationToken cancellationToken);

        public abstract Point CallOutPointOnTargetingGrid();

        public Answer AnswerToAttacker(Point attackerPoint)
            => _oceanGrid.TryHit(attackerPoint);

        public void SetDefenderAnswer(Point attackerPoint, Answer answer) =>
            _targetingGrid.SetAnswer(attackerPoint, answer);

        public bool AllShipsSunk()
            => _oceanGrid.AllShipsSunk(_allowedShips.Select(s => s.Length));

        public abstract void PrintOceanGrid();

        public abstract void PrintTargetingGrind();
    }
}
