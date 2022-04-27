namespace Battleships.Domain.Players
{
    using Battleships.Domain.Ships;
    using System.Threading;
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Grids;
    using System;

    public class ComputerPlayer : Player
    {
        public ComputerPlayer(string playerName) : base(playerName) { }

        public override void PlaceShipsOnOceanGrid(CancellationToken cancellationToken)
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

        public override Point CallOutPointOnTargetingGrid()
        {
            var random = new Random();
            Point point;

            do
            {
                point = new Point(
                    random.Next(0, Grid.Size),
                    random.Next(0, Grid.Size));

            } while (_targetingGrid.CalledOut(point));

            return point;
        }

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