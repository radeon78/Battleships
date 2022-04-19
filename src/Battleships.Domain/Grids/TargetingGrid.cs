namespace Battleships.Domain.Grids
{
    using Battleships.Domain.Players;
    using System;

    public class TargetingGrid
    {
        internal TargetingGrid()
        {
            TargetingPoints = new TargetingPoint[Grid.Size, Grid.Size];

            for (var i = 0; i < Grid.Size; i++)
            {
                for (var j = 0; j < Grid.Size; j++)
                    TargetingPoints[i, j] = new TargetingPoint();
            }
        }

        internal TargetingGrid(TargetingGrid targetingGrid)
        {
            TargetingPoints = new TargetingPoint[Grid.Size, Grid.Size];

            for (var i = 0; i < Grid.Size; i++)
            {
                for (var j = 0; j < Grid.Size; j++)
                    TargetingPoints[i, j] = new TargetingPoint(targetingGrid.TargetingPoints[i, j]);
            }
        }

        public TargetingPoint[,] TargetingPoints { get; }

        internal void SetAnswer(Point attackerPoint, Answer answer)
            => TargetingPoints[attackerPoint.Column, attackerPoint.Row].SetAnswer(answer);

        internal bool CalledOut(Point point)
            => TargetingPoints[point.Column, point.Row].CalledOut();

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var otherTargetingGrid = (TargetingGrid)obj;

            for (var i = 0; i < Grid.Size; i++)
            {
                for (var j = 0; j < Grid.Size; j++)
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

            for (var i = 0; i < Grid.Size; i++)
            {
                for (var j = 0; j < Grid.Size; j++)
                    hash.Add(TargetingPoints[i, j].GetHashCode());
            }

            return hash.ToHashCode();
        }
    }
}
