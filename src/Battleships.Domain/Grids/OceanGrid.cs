namespace Battleships.Domain.Grids;

using Battleships.Domain.Common;
using Battleships.Domain.Extensions;
using Battleships.Domain.Players;
using Battleships.Domain.Resources;
using Battleships.Domain.Ships;
using System.Collections.Generic;
using System.Linq;

public class OceanGrid
{
    private readonly List<int> _sunkShips;
    private readonly OceanPoint[,] _oceanPoints;

    internal OceanGrid()
    {
        _sunkShips = new List<int>();
        _oceanPoints = new OceanPoint[Grid.Size, Grid.Size];

        for (var i = 0; i < Grid.Size; i++)
        {
            for (var j = 0; j < Grid.Size; j++)
                _oceanPoints[i, j] = new OceanPoint();
        }
    }

    public OceanPoint this[int column, int row] => _oceanPoints[column, row];

    internal Result TryPlaceShip(StartPoint startPoint, Ship ship)
    {
        if (startPoint.Point.PointIsOutOfGrid())
            return Result.Failure(string.Format(Resource.ErrorStartingPoint, ship, startPoint.Point));

        var pointsToSelect = new List<Point>();
        var currentPoint = startPoint.Point;

        for (var i = 1; i <= ship.Length; i++)
        {
            if (CanSelectPoint(currentPoint)) pointsToSelect.Add(currentPoint);
            else return Result.Failure(string.Format(Resource.ErrorSelectPoint, ship, currentPoint));

            if (i == ship.Length) continue;

            var nextPointResult = GetNextPoint(currentPoint, startPoint.ShipPosition);
            if (nextPointResult.IsSuccess) currentPoint = nextPointResult.Data!;
            else return Result.Failure(string.Format(Resource.ErrorGetNextPoint, ship, nextPointResult.ErrorMessage));
        }

        pointsToSelect.ForEach(pointToSelect => _oceanPoints[pointToSelect.Column, pointToSelect.Row].Put(ship));
        return Result.Success();
    }

    internal Answer TryHit(Point point)
    {
        var answer = _oceanPoints[point.Column, point.Row].TryHit();
        if(answer.Reply == Reply.Sunk)
            _sunkShips.Add(answer.ShipLength);

        return answer;
    }

    internal bool AllShipsSunk(int[] allowedShipsInPlayRule)
        => allowedShipsInPlayRule.Length <= _sunkShips.Count && !allowedShipsInPlayRule.Except(_sunkShips).Any();

    private bool CanSelectPoint(Point currentPoint)
        => _oceanPoints[currentPoint.Column, currentPoint.Row].NotFillOut();

    private static Result<Point> GetNextPoint(Point currentPoint, ShipPosition shipPosition)
    {
        var nextPoint = shipPosition == ShipPosition.Horizontal
            ? new Point(currentPoint.Column + 1, currentPoint.Row)
            : new Point(currentPoint.Column, currentPoint.Row + 1);

        return nextPoint.Column > Grid.Size - 1 || nextPoint.Row > Grid.Size - 1
            ? Result.Failure<Point>(string.Format(Resource.ErrorNextPointIsOffGrid, nextPoint))
            : Result.Success(nextPoint);
    }
}