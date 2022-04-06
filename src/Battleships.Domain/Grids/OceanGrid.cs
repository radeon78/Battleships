namespace Battleships.Domain.Grids
{
    using Battleships.Domain.Common;
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Players;
    using Battleships.Domain.Ships;
    using System.Collections.Generic;
    using System.Linq;

    public class OceanGrid : Grid
    {
        private readonly List<int> _sunkShips;

        public OceanGrid()
        {
            _sunkShips = new List<int>();
            OceanPoints = new OceanPoint[Size, Size];

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                    OceanPoints[i, j] = new OceanPoint();
            }
        }

        public OceanGrid(OceanGrid oceanGrid)
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

        public Result TryPlaceShip(StartPoint startPoint, Ship ship)
        {
            if (PointIsOutOfRange(startPoint.Point))
                return Result.Failure(string.Format(Resource.ErrorStartingPoint, ship, startPoint.Point));

            var pointsToSelect = new List<Point>();
            var currentPoint = startPoint.Point;

            for (var i = 1; i <= ship.Length; i++)
            {
                if (CanSelectPoint(currentPoint)) pointsToSelect.Add(currentPoint);
                else return Result.Failure(string.Format(Resource.ErrorSelectPoint, ship, currentPoint));

                if (i < ship.Length)
                {
                    var nextPointResult = GetNextPoint(currentPoint, startPoint.Direction);
                    if (nextPointResult.IsSuccess) currentPoint = nextPointResult.Data!;
                    else return Result.Failure(string.Format(Resource.ErrorGetNextPoint, ship, nextPointResult.ErrorMessage));
                }
            }

            pointsToSelect.ForEach(pointToSelect => OceanPoints[pointToSelect.Column, pointToSelect.Row].Put(ship));
            return Result.Success();
        }

        public Answer TryHit(Point point)
        {
            var answer = OceanPoints[point.Column, point.Row].TryHit();
            (answer.Reply == Reply.Sunk).IfTrue(() => _sunkShips.Add(answer.ShipLength));

            return answer;
        }

        public bool AllShipsSunk(IEnumerable<int> allowedShipsInPlayRule)
            => allowedShipsInPlayRule.Count() <= _sunkShips.Count && !allowedShipsInPlayRule.Except(_sunkShips).Any();

        public bool PointIsOutOfRange(Point point)
        {
            return point.Column > Size - 1 ||
                point.Column < 0 ||
                point.Row > Size - 1 ||
                point.Row < 0;
        }

        private bool CanSelectPoint(Point currentPoint)
        {
            return OceanPoints[currentPoint.Column, currentPoint.Row].IsNotFillOut() &&
               OceanPoints[currentPoint.Column, FixRowOrColumnValueIfNeed(currentPoint.Row - 1)].IsNotFillOut() &&
               OceanPoints[FixRowOrColumnValueIfNeed(currentPoint.Column - 1), currentPoint.Row].IsNotFillOut() &&
               OceanPoints[FixRowOrColumnValueIfNeed(currentPoint.Column - 1), FixRowOrColumnValueIfNeed(currentPoint.Row - 1)].IsNotFillOut() &&
               OceanPoints[currentPoint.Column, FixRowOrColumnValueIfNeed(currentPoint.Row + 1)].IsNotFillOut() &&
               OceanPoints[FixRowOrColumnValueIfNeed(currentPoint.Column + 1), currentPoint.Row].IsNotFillOut() &&
               OceanPoints[FixRowOrColumnValueIfNeed(currentPoint.Column + 1), FixRowOrColumnValueIfNeed(currentPoint.Row + 1)].IsNotFillOut();
        }

        private int FixRowOrColumnValueIfNeed(int value)
        {
            return value < 0
                ? 0
                : value > Size - 1
                    ? Size - 1
                    : value;
        }

        private Result<Point> GetNextPoint(Point currentPoint, Direction direction)
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
