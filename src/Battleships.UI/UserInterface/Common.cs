namespace Battleships.UI.UserInterface
{
    using System;

    public static class Common
    {
        public static void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{message}");
            Console.ResetColor();
        }

        public static int GetColumn()
        {
            Console.WriteLine($"\nType column (A-J): ");
            ConsoleKeyInfo columnKey;
            do
            {
                columnKey = Console.ReadKey();
            }
            while ((int)columnKey.Key is < 65 or > 74);
            var column = (int)columnKey.Key - 65;

            return column;
        }

        public static int GetRow()
        {
            Console.WriteLine($"\nType row (1-10): ");
            string? input;

            do
            {
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input) && input.Length <= 2)
                {
                    if (int.TryParse(input, out var result) && result >= 1 && result <= 10)
                        return result - 1;
                }
            }
            while (true);
        }
    }
}
