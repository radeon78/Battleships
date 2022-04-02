namespace Battleships.Domain.Grids
{
    using Battleships.Domain.Common;
    using Battleships.Domain.Players;
    using Battleships.Domain.Ships;
    using System.Collections.Generic;

    public class OceanGrid : Grid
    {
        private readonly OceanPoint[,] _oceanPoints;

        public OceanGrid()
        {
            _oceanPoints = new OceanPoint[Size, Size];

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                    _oceanPoints[i, j] = new OceanPoint();
            }
        }

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

            pointsToSelect.ForEach(pointToSelect => _oceanPoints[pointToSelect.Column, pointToSelect.Row].Put(ship));
            return Result.Success();
        }

        public Result<Answer> TryHit(Point point)
        {
            return PointIsOutOfRange(point) == true
                ? Result.Failure<Answer>(string.Format(Resource.ErrorPointIsOffGrid, point))
                : Result.Success(_oceanPoints[point.Column, point.Row].TryHit());
        }

        public bool PointIsOutOfRange(Point point)
        {
            return point.Column > Size - 1 ||
                point.Column < 0 ||
                point.Row > Size - 1 ||
                point.Row < 0;
        }

        private bool CanSelectPoint(Point currentPoint)
        {
            return _oceanPoints[currentPoint.Column, currentPoint.Row].IsNotFillOut() &&
               _oceanPoints[currentPoint.Column, FixRowOrColumnValueIfNeed(currentPoint.Row - 1)].IsNotFillOut() &&
               _oceanPoints[FixRowOrColumnValueIfNeed(currentPoint.Column - 1), currentPoint.Row].IsNotFillOut() &&
               _oceanPoints[FixRowOrColumnValueIfNeed(currentPoint.Column - 1), FixRowOrColumnValueIfNeed(currentPoint.Row - 1)].IsNotFillOut() &&
               _oceanPoints[currentPoint.Column, FixRowOrColumnValueIfNeed(currentPoint.Row + 1)].IsNotFillOut() &&
               _oceanPoints[FixRowOrColumnValueIfNeed(currentPoint.Column + 1), currentPoint.Row].IsNotFillOut() &&
               _oceanPoints[FixRowOrColumnValueIfNeed(currentPoint.Column + 1), FixRowOrColumnValueIfNeed(currentPoint.Row + 1)].IsNotFillOut();
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
