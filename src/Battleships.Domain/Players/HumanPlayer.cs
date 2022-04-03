﻿namespace Battleships.Domain.Players
{
    using Battleships.Domain.Common;
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Grids;
    using System;
    using System.Threading;

    public class HumanPlayer : Player
    {
        private readonly Func<string, StartPoint> _getPlaceShipStartPoint;
        private readonly Func<string, Point> _callOutPointOnTargetingGrid;
        private readonly Action<string> _printErrorMessage;

        public HumanPlayer(
            string playerName,
            Func<string, StartPoint> getPlaceShipStartPoint,
            Func<string, Point> callOutPointOnTargetingGrid,
            Action<string> printErrorMessage) : base(playerName)
        {
            _getPlaceShipStartPoint = getPlaceShipStartPoint;
            _callOutPointOnTargetingGrid = callOutPointOnTargetingGrid;
            _printErrorMessage = printErrorMessage;
        }

        public override void PlaceShipsOnOceanGrid(CancellationToken cancellationToken)
        {
            _allowedShips.ForEach(ship =>
            {
                Result result;
                do
                {
                    if (cancellationToken.IsCancellationRequested) return;

                    var startPoint = _getPlaceShipStartPoint(string.Format(Resource.PlaceShipMessage, _playerName, ship));
                    result = _oceanGrid.TryPlaceShip(startPoint, ship);
                    if (result.IsFailure) _printErrorMessage(result.ErrorMessage);
                }
                while (result.IsFailure);
            }, cancellationToken);
        }

        public override Point CallOutPointOnTargetingGrid()
        {
            Point point;
            bool pointOutOfRange;

            do
            {
                point = _callOutPointOnTargetingGrid(string.Format(Resource.CallOutPositionOnTargetingGrid, _playerName));
                pointOutOfRange = _oceanGrid.PointIsOutOfRange(point);
                if (pointOutOfRange) _printErrorMessage(string.Format(Resource.ErrorPointIsOffGrid, point));
            }
            while (pointOutOfRange);

            return point;
        }
    }
}
