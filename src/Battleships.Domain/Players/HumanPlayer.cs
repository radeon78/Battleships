namespace Battleships.Domain.Players
{
    using Battleships.Domain.Common;
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Grids;
    using Battleships.Domain.Resources;
    using System;
    using System.Threading;

    public class HumanPlayer : Player
    {
        private readonly Func<string, StartPoint> _getPlaceShipStartPoint;
        private readonly Action<string, OceanGrid> _printOceanGrid;
        private readonly Func<string, Point> _callOutPointOnTargetingGrid;
        private readonly Action<string, TargetingGrid> _printTargetingGrid;
        private readonly Action<string> _printErrorMessage;

        public HumanPlayer(
            string playerName,
            Func<string, StartPoint> getPlaceShipStartPoint,
            Action<string, OceanGrid> printOceanGrid,
            Func<string, Point> callOutPointOnTargetingGrid,
            Action<string, TargetingGrid> printTargetingGrid,
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
                    var startPoint = _getPlaceShipStartPoint(string.Format(Resource.PlaceShipMessage, _playerName, ship));
                    result = _oceanGrid.TryPlaceShip(startPoint, ship);

                    if (cancellationToken.IsCancellationRequested) return;

                    result.IsSuccess
                        .IfTrue(() => PrintOceanGrid())
                        .IfFalse(() => _printErrorMessage(result.ErrorMessage));
                }
                while (result.IsFailure);
            }, cancellationToken);
        }

        public override Point CallOutPointOnTargetingGrid()
        {
            Point point;
            bool pointOutOfGrid;

            do
            {
                point = _callOutPointOnTargetingGrid(string.Format(Resource.CallOutPositionOnTargetingGrid, _playerName));
                pointOutOfGrid = point.PointIsOutOfGrid();
                pointOutOfGrid
                    .IfTrue(() => _printErrorMessage(string.Format(Resource.ErrorPointIsOffGrid, point)));
            }
            while (pointOutOfGrid);

            return point;
        }

        public override void PrintOceanGrid()
            => _printOceanGrid(_playerName, _oceanGrid);

        public override void PrintTargetingGrind()
            => _printTargetingGrid(_playerName, _targetingGrid);
    }
}
