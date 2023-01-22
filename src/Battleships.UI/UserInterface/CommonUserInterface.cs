using Battleships.Domain.Extensions;
using Battleships.Domain.Grids;
using Battleships.UI.Resources;
using ConsoleTables;

namespace Battleships.UI.UserInterface;

public static class CommonUserInterface
{
    public static string GetHumanPlayerName(CancellationTokenSource tokenSource)
        => GetInputDataFromUser(
            displayMessageToUser: Resource.TypeName,
            inputValid: input => !string.IsNullOrEmpty(input),
            tokenSource: tokenSource);

    public static string GetInputDataFromUser(
        string displayMessageToUser,
        Func<string, bool> inputValid,
        CancellationTokenSource tokenSource)
    {
        Console.WriteLine(Environment.NewLine + displayMessageToUser);
        const string quitKey = "q";

        do
        {
            var input = Console.ReadLine() ?? string.Empty;

            if (input.Equals(quitKey, StringComparison.OrdinalIgnoreCase))
            {
                tokenSource.Cancel();
                return string.Empty;
            }

            if (inputValid(input))
            {
                return input;
            }
        }
        while (true);
    }

    public static void PrintErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(Environment.NewLine + message);
        Console.ResetColor();
    }

    public static void PrintGrid(Func<int, int, string> getPointStatus)
    {
        var columns = new string[Grid.Size + 1];

        columns[0] = string.Empty;
        for (var i = 0; i < Grid.Size; i++)
            columns[i + 1] = i.ToDisplayColumnAsString();

        var table = new ConsoleTable(columns);

        for (var i = 0; i < Grid.Size; i++)
        {
            var row = new string[Grid.Size + 1];
            row[0] = i.ToDisplayRow();

            for (var j = 0; j < Grid.Size; j++)
                row[j + 1] = getPointStatus(j, i);

            table.AddRow(row);
        }

        table.Write();
    }
}