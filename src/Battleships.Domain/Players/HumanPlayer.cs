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
        private readonly Action<OceanGrid> _printOceanGrid;
        private readonly Func<string, Point> _callOutPointOnTargetingGrid;
        private readonly Action<TargetingGrid> _printTargetingGrid;
        private readonly Action<string> _printErrorMessage;

        public HumanPlayer(
            string playerName,
            Func<string, StartPoint> getPlaceShipStartPoint,
            Action<OceanGrid> printOceanGrid,
            Func<string, Point> callOutPointOnTargetingGrid,
            Action<TargetingGrid> printTargetingGrid,
            Action<string> printErrorMessage) : base(playerName)
        {
            _getPlaceShipStartPoint = getPlaceShipStartPoint ?? throw new ArgumentNullException(nameof(getPlaceShipStartPoint));
            _printOceanGrid = printOceanGrid ?? throw new ArgumentNullException(nameof(printOceanGrid));
            _callOutPointOnTargetingGrid = callOutPointOnTargetingGrid ?? throw new ArgumentNullException(nameof(callOutPointOnTargetingGrid));
            _printTargetingGrid = printTargetingGrid ?? throw new ArgumentNullException(nameof(printTargetingGrid));
            _printErrorMessage = printErrorMessage ?? throw new ArgumentNullException(nameof(printErrorMessage));
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

        public override void PrintOceanGrid()
            => _printOceanGrid(new OceanGrid(_oceanGrid));

        public override void PrintTargetingGrind()
            => _printTargetingGrid(new TargetingGrid(_targetingGrid));
    }
}
