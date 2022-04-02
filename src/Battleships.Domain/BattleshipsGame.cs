namespace Battleships.Domain
{
    using Battleships.Domain.Players;
    using Battleships.Domain.PlayRules;
    using System;
    using System.Threading;

    public class BattleshipsGame
    {
        private readonly IPlayRule _playRule;
        private readonly Action<string> _printMessage;

        public BattleshipsGame(
            IPlayRule playRule,
            Action<string> printMessage)
        {
            _playRule = playRule;
            _printMessage = printMessage;
        }

        public void StartGame(
            IPlayer firstPlayer,
            IPlayer secondPlayer,
            CancellationToken cancellationToken)
        {
            firstPlayer.ApplyGameRule(_playRule);
            secondPlayer.ApplyGameRule(_playRule);

            _printMessage(Resource.WelcomeGame);

            firstPlayer.PlaceShipsOnGrid();
            secondPlayer.PlaceShipsOnGrid();

            _printMessage(Resource.ShipsOnGrid);
        }
    }
}
