namespace Battleships.Domain.Grids
{
    using Battleships.Domain.Common;
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Players;
    using Battleships.Domain.Resources;
    using Battleships.Domain.Ships;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OceanGrid : Grid
    {
        private readonly List<int> _sunkShips;

        internal OceanGrid()
        {
            _sunkShips = new List<int>();
            OceanPoints = new OceanPoint[Size, Size];

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                    OceanPoints[i, j] = new OceanPoint();
            }
        }

        internal OceanGrid(OceanGrid oceanGrid)
        {
            _sunkShips = oceanGrid._sunkShips.Select(x => x).ToList();
            OceanPoints = new OceanPoint[Size, Size];

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                    OceanPoints[i, j] = new OceanPoint(oceanGrid.OceanPoints[i, j]);
            }
        }

        public OceanPoint[,] OceanPoints { get; }

        internal Result TryPlaceShip(StartPoint startPoint, Ship ship)
        {
            if (PointIsOutOfGrid(startPoint.Point))
                return Result.Failure(string.Format(Resource.ErrorStartingPoint, ship, startPoint.Point));

            var pointsToSelect = new List<Point>();
            var currentPoint = startPoint.Point;

            for (var i = 1; i <= ship.Length; i++)
            {
                if (CanSelectPoint(currentPoint)) pointsToSelect.Add(currentPoint);
                else return Result.Failure(string.Format(Resource.ErrorSelectPoint, ship, currentPoint));

                if (i == ship.Length) continue;

                var nextPointResult = GetNextPoint(currentPoint, startPoint.Direction);
                if (nextPointResult.IsSuccess) currentPoint = nextPointResult.Data!;
                else return Result.Failure(string.Format(Resource.ErrorGetNextPoint, ship, nextPointResult.ErrorMessage));
            }

            pointsToSelect.ForEach(pointToSelect => OceanPoints[pointToSelect.Column, pointToSelect.Row].Put(ship));
            return Result.Success();
        }

        internal Answer TryHit(Point point)
        {
            var answer = OceanPoints[point.Column, point.Row].TryHit();
            (answer.Reply == Reply.Sunk).IfTrue(() => _sunkShips.Add(answer.ShipLength));

            return answer;
        }

        internal bool AllShipsSunk(int[] allowedShipsInPlayRule)
            => allowedShipsInPlayRule.Length <= _sunkShips.Count && !allowedShipsInPlayRule.Except(_sunkShips).Any();

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
                return false;

            var otherOceanGrid = (OceanGrid)obj;

            if (!_sunkShips.SequenceEqual(otherOceanGrid._sunkShips))
                return false;

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    if (!OceanPoints[i, j].Equals(otherOceanGrid.OceanPoints[i, j]))
                        return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            _sunkShips.ForEach(sunkShip => hash.Add(sunkShip.GetHashCode()));

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                    hash.Add(OceanPoints[i, j].GetHashCode());
            }

            return hash.ToHashCode();
        }

        private bool CanSelectPoint(Point currentPoint)
            => OceanPoints[currentPoint.Column, currentPoint.Row].NotFillOut();

        private static Result<Point> GetNextPoint(Point currentPoint, Direction direction)
        {
            var nextPoint = direction == Direction.Horizontal
                ? new Point(currentPoint.Column + 1, currentPoint.Row)
                : new Point(currentPoint.Column, currentPoint.Row + 1);

            return nextPoint.Column > Size - 1 || nextPoint.Row > Size - 1
                ? Result.Failure<Point>(string.Format(Resource.ErrorNextPointIsOffGrid, nextPoint))
                : Result.Success(nextPoint);
        }
    }
}
