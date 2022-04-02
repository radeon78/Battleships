namespace Battleships.Domain.Grids
{
    using Battleships.Domain.Players;

    public class TargetingGrid : Grid
    {
        private readonly TargetingPoint[,] _targetingPoints;

        public TargetingGrid()
        {
            _targetingPoints = new TargetingPoint[Size, Size];

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                    _targetingPoints[i, j] = new TargetingPoint();
            }

        }

        public void SetAnswer(Point attackerPoint, Answer answer)
            => _targetingPoints[attackerPoint.Column, attackerPoint.Row].SetAnswer(answer);
    }
}
