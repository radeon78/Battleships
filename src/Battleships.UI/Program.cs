using Battleships.Domain;
using Battleships.UI;

var tokenSource = new CancellationTokenSource();
var token = tokenSource.Token;

var game = new BattleshipsGame(
    gameRule: new ThreeShipsGameRule(),
    printMessage: message => Console.WriteLine(Environment.NewLine + message));

game.Start(
    firstPlayer: PlayerFactory.CreateHumanPlayer(tokenSource),
    secondPlayer: PlayerFactory.CreateComputerPlayer("Computer"),
    cancellationToken: token);
