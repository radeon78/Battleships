namespace Battleships.Domain.Players
{
    using Battleships.Domain.Extensions;
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

        protected Player(string playerName)
        {
            _playerName = playerName.NonEmpty(nameof(playerName));
            _oceanGrid = new OceanGrid();
            _targetingGrid = new TargetingGrid();
            _allowedShips = Array.Empty<Ship>();
        }

        public string PlayerName => _playerName;

        public void ApplyGameRule(IGameRule gameRule)
            => _allowedShips = gameRule.GetAllowedShips();

        public abstract void PlaceShipsOnOceanGrid(CancellationToken cancellationToken);

        public abstract Point CallOutPointOnTargetingGrid();

        public void SetDefenderAnswer(Point attackerPoint, Answer answer) =>
            _targetingGrid.SetAnswer(attackerPoint, answer);

        public virtual void PrintTargetingGrind() { }

        public Answer AnswerToAttacker(Point attackerPoint)
            => _oceanGrid.TryHit(attackerPoint);

        public bool AllShipsSunk()
            => _oceanGrid.AllShipsSunk(_allowedShips.Select(s => s.Length).ToArray());

        public virtual void PrintOceanGrid() { }
    }
}
