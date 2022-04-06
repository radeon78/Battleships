namespace Battleships.Domain.Players
{
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Grids;
    using System;
    using System.Threading;

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

        public StartPoint GenerateRandomPlaceShipStartPoint()
        {
            var random = new Random();

            var point = new Point(
                random.Next(0, _oceanGrid.Size),
                random.Next(0, _oceanGrid.Size));

            var direction = (Direction)random.Next(0, 1);

            return new StartPoint(point, direction);
        }

        public override Point CallOutPointOnTargetingGrid()
        {
            var random = new Random();

            return new Point(
                random.Next(0, _oceanGrid.Size),
                random.Next(0, _oceanGrid.Size));
        }

        public override void PrintOceanGrid() 
        {
            // We don't have to print ships for computer player
        }
    }
}
