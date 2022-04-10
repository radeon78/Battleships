namespace Battleships.Domain.Players
{
    using Battleships.Domain.Grids;

    public interface IAttackerPlayer
    {
        Point CallOutPointOnTargetingGrid();

        void SetDefenderAnswer(Point attackerPoint, Answer answer);

        void PrintTargetingGrind();
    }
}
