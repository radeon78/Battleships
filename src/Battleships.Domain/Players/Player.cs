namespace Battleships.Domain.Players
{
    using Battleships.Domain.Common;
    using Battleships.Domain.Grids;
    using Battleships.Domain.PlayRules;
    using Battleships.Domain.Ships;
    using System;
    using System.Collections.Generic;
    using System.Threading;

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

        public string PlayerName => _playerName;

        public void ApplyGameRule(IPlayRule playRule)
            => _allowedShips = playRule.GetAllowedShips();

        public abstract void PlaceShipsOnOceanGrid(CancellationToken cancellationToken);

        public abstract Point CallOutPointOnTargetingGrid();

        public virtual Result<Answer> AnswerToAttacker(Point attackerPoint)
        {
            var shipHitResult = _oceanGrid.TryHit(attackerPoint);

            return shipHitResult.IsFailure
                ? Result.Failure<Answer>(shipHitResult.ErrorMessage)
                : Result.Success(shipHitResult.Data!);
        }

        public void SetDefenderAnswer(Point attackerPoint, Answer answer) =>
            _targetingGrid.SetAnswer(attackerPoint, answer);

        public bool AllShipsSunk() => throw new NotImplementedException();
    }
}
