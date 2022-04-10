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
        {
            Console.WriteLine($"\nType your name: ");
            string? input;

            do
            {
                input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input))
                {
                    if (input.Equals("q", StringComparison.OrdinalIgnoreCase)) 
                        tokenSource.Cancel();

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

        public static int GetColumn(CancellationTokenSource tokenSource)
        {
            Console.WriteLine($"\nType column (A-J): ");
            ConsoleKeyInfo columnKey;
            do
            {
                columnKey = Console.ReadKey();
                if (columnKey.Key.ToString().Equals("q", StringComparison.OrdinalIgnoreCase))
                {
                    tokenSource.Cancel();
                    return 0;
                } 
            }
            while ((int)columnKey.Key is < 65 or > 74);
            var column = (int)columnKey.Key - 65;

            return column;
        }

        public static int GetRow(CancellationTokenSource tokenSource)
        {
            Console.WriteLine($"\nType row (1-10): ");
            string? input;

            do
            {
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input) && input.Length <= 2)
                {
                    if (input.Equals("q", StringComparison.OrdinalIgnoreCase))
                    {
                        tokenSource.Cancel();
                        return 0;
                    }

                    if (int.TryParse(input, out var result) && result >= 1 && result <= 10)
                        return result - 1;
                }
            }
            while (true);
        }

        public static void PrintGrid(this Grid grid, Func<int, int, string> getPointStatus)
        {
            var columns = new string[grid.Size + 1];

            columns[0] = string.Empty;
            for (var i = 0; i < grid.Size; i++)
                columns[i + 1] = i.ToDisplayColumnAsString();

            var table = new ConsoleTable(columns);

            for (var i = 0; i < grid.Size; i++)
            {
                var row = new string[grid.Size + 1];
                row[0] = i.ToDisplayRow();

                for (var j = 0; j < grid.Size; j++)
                    row[j + 1] = getPointStatus(j, i);

                table.AddRow(row);
            }

            table.Write();
        }
    }
}
