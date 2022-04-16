namespace Battleships.UI.UserInterface
{
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Grids;
    using ConsoleTables;
    using System;
    using System.Threading;

    public static class CommonUserInterface
    {
        public static string GetHumanPlayerName(CancellationTokenSource tokenSource)
            => GetInputDataFromUser(
                displayMessageToUser: "\nType your name: ",
                inputValid: input => !string.IsNullOrEmpty(input),
                tokenSource: tokenSource);

        public static string GetInputDataFromUser(
            string displayMessageToUser,
            Func<string, bool> inputValid,
            CancellationTokenSource tokenSource)
        {
            Console.WriteLine(displayMessageToUser);

            do
            {
                var input = Console.ReadLine() ?? string.Empty;

                if (input.Equals("q", StringComparison.OrdinalIgnoreCase))
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
            Console.WriteLine($"\n{message}");
            Console.ResetColor();
        }

        public static void PrintGrid(this Grid grid, Func<int, int, string> getPointStatus)
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
}
