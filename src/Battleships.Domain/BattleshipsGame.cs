namespace Battleships.Domain
{
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Players;
    using Battleships.Domain.Resources;
    using System;
    using System.Threading;

    public class BattleshipsGame
    {
        private readonly IGameRule _gameRule;
        private readonly Action<string> _printMessage;

        public BattleshipsGame(
            IGameRule gameRule,
            Action<string> printMessage)
        {
            _gameRule = gameRule ?? throw new ArgumentNullException(nameof(gameRule));
            _printMessage = printMessage ?? throw new ArgumentNullException(nameof(printMessage));
        }

        public void Start(
            Player firstPlayer,
            Player secondPlayer,
            CancellationToken cancellationToken)
        {
            firstPlayer.ApplyGameRule(_gameRule);
            secondPlayer.ApplyGameRule(_gameRule);

            _printMessage(Resource.WelcomeGame);
            _printMessage(_gameRule.GetGameRuleDescription());
            _printMessage(Resource.QuitTheGame);

            firstPlayer.PlaceShipsOnOceanGrid(cancellationToken);
            firstPlayer.PrintOceanGrid();
            secondPlayer.PlaceShipsOnOceanGrid(cancellationToken);
            secondPlayer.PrintOceanGrid();

            _printMessage(Resource.ShipsOnGrid);

            Play(firstPlayer, secondPlayer, cancellationToken);
        }

        private void Play(
            IAttackerPlayer attacker,
            IDefenderPlayer defender,
            CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return;

            attacker.PrintTargetingGrind();

            var attackerPoint = attacker.CallOutPointOnTargetingGrid();
            var defenderAnswer = defender.AnswerToAttacker(attackerPoint);
            attacker.SetDefenderAnswer(attackerPoint, defenderAnswer);

            (defenderAnswer.Reply == Reply.Hit || defenderAnswer.Reply == Reply.Sunk)
                .IfTrue(() => defender.PrintOceanGrid());

            _printMessage($"{attacker.PlayerName} (attacker) called out {attackerPoint}");
            _printMessage($"{defender.PlayerName} (defender) answered {defenderAnswer}");

            if (defender.AllShipsSunk())
            {
                _printMessage(string.Format(Resource.GameEnded, attacker.PlayerName));
                return;
            }

            Play((IAttackerPlayer)defender, (IDefenderPlayer)attacker, cancellationToken);
        }
    }
}
