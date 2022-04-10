namespace Battleships.Domain.Grids
{
    using Battleships.Domain.Players;
    using System;

    public class TargetingGrid : Grid
    {
        public TargetingGrid()
        {
            TargetingPoints = new TargetingPoint[Size, Size];

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                    TargetingPoints[i, j] = new TargetingPoint();
            }
        }

        public TargetingGrid(TargetingGrid targetingGrid)
        {
            TargetingPoints = new TargetingPoint[Size, Size];

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                    TargetingPoints[i, j] = new TargetingPoint(targetingGrid.TargetingPoints[i, j]);
            }
        }

        public TargetingPoint[,] TargetingPoints { get; }

        public void SetAnswer(Point attackerPoint, Answer answer)
            => TargetingPoints[attackerPoint.Column, attackerPoint.Row].SetAnswer(answer);

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
                return false;

            var otherTargetingGrid = (TargetingGrid)obj;

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    if (!TargetingPoints[i, j].Equals(otherTargetingGrid.TargetingPoints[i, j]))
                        return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                    hash.Add(TargetingPoints[i, j].GetHashCode());
            }

            return hash.ToHashCode();
        }
    }
}
