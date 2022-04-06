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

            firstPlayer.PlaceShipsOnOceanGrid(cancellationToken);
            firstPlayer.PrintOceanGrid();
            secondPlayer.PlaceShipsOnOceanGrid(cancellationToken);
            secondPlayer.PrintOceanGrid();

            _printMessage(Resource.ShipsOnGrid);

            PlayGame(firstPlayer, secondPlayer, cancellationToken);
        }

        private void PlayGame(
            IPlayer attacker,
            IPlayer defender,
            CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return;

            attacker.PrintTargetingGrind();

            var attackerPoint = attacker.CallOutPointOnTargetingGrid();
            var defenderAnswer = defender.AnswerToAttacker(attackerPoint);
            attacker.SetDefenderAnswer(attackerPoint, defenderAnswer);
            var endGame = defender.AllShipsSunk();

            defender.PrintOceanGrid();

            if (endGame)
            {
                _printMessage(string.Format(Resource.GameEnded, attacker.PlayerName));
                return;
            }

            PlayGame(defender, attacker, cancellationToken);
        }
    }
}
