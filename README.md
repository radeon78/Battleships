# Battleships
Battleships game for two players a single human player and computer player.

You can configure the game to play a computer player against a computer player as well. 
Furthermore you can configure the game to play a human player against another human player.

The game allow to define count of ships in your fleet  and kind of each ship.

## Pre-requisites

Application requires .NET 6 installed. You can download it from https://dotnet.microsoft.com/en-us/download/dotnet/6.0

## Get Started

### Clone repo locally

```powershell
git clone https://github.com/radeon78/Battleships.git
```

### Run the game

The default configuration is to game a single human player and computer player with fleet of three ships one Carrier (5 squares) one Battleship (4 squares) and one Destroyer (2 squares).

```powershell
dotnet run --project ./battleships/src/battleships.ui/battleships.ui.csproj
```

## Custom configuring

### Define own game rule

You can define custom game rule by implementing interface IGameRule

```csharp
public interface IGameRule
{
    public IReadOnlyCollection<Ship> GetAllowedShips();

    public string GetGameRuleDescription();
}
```

e.g. Defining fleet with two ships one Carrier (5 squares) and one Submarine (3 squares)

```csharp
public class TwoShipsGameRule : IGameRule
{
    public IReadOnlyCollection<Ship> GetAllowedShips()
        => Array.AsReadOnly(
            new Ship[]
            {
                CreateCarrier(),
                CreateSubmarine()
            });
    public string GetGameRuleDescription()
        => @"Secretly place your fleet of two on your ocean grid. Your opponent does the same.
Rules for placing ships:
  - Place each ship in any horizontal or vertical position. For each ship you will be asked to type a starting point and position then application calculate and select the rest points on ocean grid
  - Do not place a ship so that any part of it overlaps letters, numbers, the edge of the grid or another ship
  - If you are the first player to sink your opponent's entire fleet of two ships, you win the game!";

    private static Ship CreateCarrier() => new(5);

    private static Ship CreateSubmarine() => new(3);
}
```

Defined rule inject to BattleshipsGame

```csharp
...
var game = new BattleshipsGame(
    gameRule: new TwoShipsGameRule(),
    printMessage: (message) => Console.WriteLine(Environment.NewLine + message));
...
```

### Define players

Use PlayerFactory which helps in creating human and computer players. Pass created players to the Start method as parameters.

```csharp
var tokenSource = new CancellationTokenSource();
var token = tokenSource.Token;

var game = new BattleshipsGame(
    gameRule: new TwoShipsGameRule(),
    printMessage: (message) => Console.WriteLine(Environment.NewLine + message));

game.Start(
    firstPlayer: PlayerFactory.CreateHumanPlayer(tokenSource),
    secondPlayer: PlayerFactory.CreateComputerPlayer("Computer"),
    cancellationToken: token);
```