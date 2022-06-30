namespace Battleships.Domain;

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
        _gameRule = gameRule.NonNull(nameof(gameRule));
        _printMessage = printMessage.NonNull(nameof(printMessage));
    }

    public void Start(
        IPlayer firstPlayer,
        IPlayer secondPlayer,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested) return;

        firstPlayer.ApplyGameRule(_gameRule);
        secondPlayer.ApplyGameRule(_gameRule);

        _printMessage(Resource.WelcomeGame);
        _printMessage(_gameRule.GetGameRuleDescription());
        _printMessage(Resource.QuitTheGame);

        firstPlayer.PlaceShipsOnOceanGrid(cancellationToken);
        secondPlayer.PlaceShipsOnOceanGrid(cancellationToken);

        if (cancellationToken.IsCancellationRequested) return;

        _printMessage(Resource.ShipsOnGrid);

        StartRounds(
            attacker: firstPlayer,
            defender: secondPlayer,
            cancellationToken: cancellationToken);
    }

    private void StartRounds(IPlayer attacker, IPlayer defender, CancellationToken cancellationToken)
    {
        while (true)
        {
            if (cancellationToken.IsCancellationRequested) return;

            attacker.PrintTargetingGrind();

            var attackerPoint = attacker.CallOutPointOnTargetingGrid();
            if (cancellationToken.IsCancellationRequested) return;

            var defenderAnswer = defender.AnswerToAttacker(attackerPoint);
            attacker.SetDefenderAnswer(attackerPoint, defenderAnswer);

            if (defenderAnswer.Reply is Reply.Hit or Reply.Sunk) defender.PrintOceanGrid();

            _printMessage(string.Format(Resource.AttackerCalledOutPoint, attacker.PlayerName, attackerPoint));
            _printMessage(string.Format(Resource.DefenderAnswered, defender.PlayerName, defenderAnswer));

            if (defender.AllShipsSunk())
            {
                _printMessage(string.Format(Resource.GameEnded, attacker.PlayerName));
                return;
            }

            (attacker, defender) = (defender, attacker);
        }
    }
}