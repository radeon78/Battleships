﻿namespace Battleships.Domain.Players
{
    using Battleships.Domain;
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Grids;
    using Battleships.Domain.Ships;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public abstract class Player : IAttackerPlayer, IDefenderPlayer
    {
        protected readonly string _playerName;
        protected OceanGrid _oceanGrid;
        protected TargetingGrid _targetingGrid;
        protected IReadOnlyCollection<Ship> _allowedShips;

        protected Player(string playerName)
        {
            _playerName = playerName ?? throw new ArgumentNullException(nameof(playerName));
            _oceanGrid = new OceanGrid();
            _targetingGrid = new TargetingGrid();
            _allowedShips = Array.Empty<Ship>();
        }

        public virtual string PlayerName => _playerName;

        public virtual void ApplyGameRule(IGameRule gameRule)
            => _allowedShips = gameRule.GetAllowedShips();

        public virtual void PlaceShipsOnOceanGrid(CancellationToken cancellationToken)
        {
            _allowedShips.ForEach(ship =>
            {
                StartPoint startPoint;
                do
                {
                    if (cancellationToken.IsCancellationRequested) return;
                    startPoint = GenerateRandomPlaceShipStartPoint();
                }
                while (_oceanGrid.TryPlaceShip(startPoint, ship).IsFailure);
            }, cancellationToken);
        }

        public abstract Point CallOutPointOnTargetingGrid();

        public virtual Answer AnswerToAttacker(Point attackerPoint)
            => _oceanGrid.TryHit(attackerPoint);

        public virtual void SetDefenderAnswer(Point attackerPoint, Answer answer) =>
            _targetingGrid.SetAnswer(attackerPoint, answer);

        public virtual bool AllShipsSunk()
            => _oceanGrid.AllShipsSunk(_allowedShips.Select(s => s.Length).ToArray());

        public virtual void PrintOceanGrid() { }

        public virtual void PrintTargetingGrind() { }

        internal static StartPoint GenerateRandomPlaceShipStartPoint()
        {
            var random = new Random();

            var point = new Point(
                random.Next(0, Grid.Size),
                random.Next(0, Grid.Size));

            var direction = (ShipPosition)random.Next(0, 1);

            return new StartPoint(point, direction);
        }
    }
}
