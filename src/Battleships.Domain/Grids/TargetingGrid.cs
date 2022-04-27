namespace Battleships.Domain.Grids;

using Battleships.Domain.Players;

public class TargetingGrid
{
    private readonly TargetingPoint[,] _targetingPoints;

    internal TargetingGrid()
    {
        _targetingPoints = new TargetingPoint[Grid.Size, Grid.Size];

        for (var i = 0; i < Grid.Size; i++)
        {
            for (var j = 0; j < Grid.Size; j++)
                _targetingPoints[i, j] = new TargetingPoint();
        }
    }

    public TargetingPoint this[int column, int row] => _targetingPoints[column, row];
        
    internal void SetAnswer(Point attackerPoint, Answer answer)
        => _targetingPoints[attackerPoint.Column, attackerPoint.Row].SetAnswer(answer);

    internal bool CalledOut(Point point)
        => _targetingPoints[point.Column, point.Row].CalledOut();
}