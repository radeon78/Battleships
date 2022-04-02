namespace Battleships.Domain.Players
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
                if (cancellationToken.IsCancellationRequested) return;

                var startPoint = _getPlaceShipStartPoint(string.Format(Resource.PlaceShipMessage, _playerName, ship));

                Result result;
                while ((result = _oceanGrid.TryPlaceShip(startPoint, ship)).IsFailure)
                {
                    if (cancellationToken.IsCancellationRequested) return;

                    _printErrorMessage(result.ErrorMessage);
                    startPoint = _getPlaceShipStartPoint(string.Format(Resource.PlaceShipMessage, _playerName, ship));
                }
            });
        }

        public override Point CallOutPointOnTargetingGrid()
        {
            var point = _callOutPointOnTargetingGrid(
                string.Format(Resource.CallOutPositionOnTargetingGrid, _playerName));

            while (_oceanGrid.PointIsOutOfRange(point))
            {
                _printErrorMessage(string.Format(Resource.ErrorPointIsOffGrid, point));

                point = _callOutPointOnTargetingGrid(
                    string.Format(Resource.CallOutPositionOnTargetingGrid, _playerName));
            }

            return point;
        }
    }
}
