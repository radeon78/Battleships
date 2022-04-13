namespace Battleships.Domain.Players
{
    using Battleships.Domain.Grids;

    public interface IAttackerPlayer : IBasePlayer
    {
        Point CallOutPointOnTargetingGrid();

        void SetDefenderAnswer(Point attackerPoint, Answer answer);

        void PrintTargetingGrind();
    }
}
