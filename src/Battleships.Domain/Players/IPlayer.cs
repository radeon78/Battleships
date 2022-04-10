namespace Battleships.Domain.Players
{
    using Battleships.Domain.PlayRules;
    using System.Threading;

    public interface IPlayer : IAttackerPlayer, IDefenderPlayer
    {
        string PlayerName { get; }

        void ApplyGameRule(IPlayRule playRule);

        void PlaceShipsOnOceanGrid(CancellationToken cancellationToken);
    }
}
