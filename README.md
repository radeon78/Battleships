# Battleships
Battleships game for two players a single human player and computer player.

You can configure the game to play a computer player against a computer player as well. 
Furthermore you can configure the game to play a human player against another human player.

The game allow to define count of ships and kind of each ship.

## Get Started

### Clone repo locally

```powershell
git clone https://github.com/radeon78/Battleships.git
```

### Run the game

The default configuration is to game a single human player and computer player with fleet of three ships one Battleship (5 squares) and two Destroyers (4 squares).

```powershell
dotnet run --project ./battleships/src/battleships.ui/battleships.ui.csproj
```

## Custom configuring

### Defining game rule

You can define custom game rule by implementing interface IGameRule

```csharp
public interface IGameRule
{
    public IReadOnlyCollection<Ship> GetAllowedShips();

    public string GetGameRuleDescription();
}
```

e.g. defining fleet with two ships one Carrier (5 squares) and one Submarine (3 squares)

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
  - Place each ship in any horizontal or vertical position. For each ship you be asked to type a starting point and position then application calculate and select the rest points on ocean grid
  - Do not place a ship so that any part of it overlaps letters, numbers, the edge of the grid or another ship
  - If you are the first player to sink your opponent's entire fleet of two ships, you win the game!";

    private static Ship CreateCarrier() => new(5);

    private static Ship CreateSubmarine() => new(3);
}
```

defined rule you can inject to BattleshipsGame

```csharp
var game = new BattleshipsGame(
    gameRule: new TwoShipsGameRule(),
    printMessage: (message) => Console.WriteLine(Environment.NewLine + message));
```
