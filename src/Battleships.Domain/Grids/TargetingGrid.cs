namespace Battleships.Domain.Grids
{
    using Battleships.Domain.Players;

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
    }
}
