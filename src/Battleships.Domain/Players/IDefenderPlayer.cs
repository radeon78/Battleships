namespace Battleships.Domain.Players
{
    using Battleships.Domain.Grids;

    public interface IDefenderPlayer : IBasePlayer
    {
        Answer AnswerToAttacker(Point attackerPoint);

        bool AllShipsSunk();

        void PrintOceanGrid();
    }
}
