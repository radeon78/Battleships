namespace Battleships.Domain.Players
{
    using Battleships.Domain;
    using System.Threading;

    public interface IBasePlayer
    {
        string PlayerName { get; }

        void ApplyGameRule(IGameRule gameRule);

        void PlaceShipsOnOceanGrid(CancellationToken cancellationToken);
    }
}
