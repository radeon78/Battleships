namespace Battleships.Domain.Players
{
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Grids;
    using System;

    public class ComputerPlayer : Player
    {
        public ComputerPlayer(string playerName) : base(playerName) { }

        public override void PlaceShipsOnGrid()
        {
            _allowedShips.ForEach(ship =>
            {
                var startPoint = GenerateRandomPlaceShipStartPoint();
                while (_oceanGrid.TryPlaceShip(startPoint, ship).IsFailure)
                    startPoint = GenerateRandomPlaceShipStartPoint();
            });
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
    }
}
