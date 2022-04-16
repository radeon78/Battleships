namespace Battleships.Domain.Extensions
{
    public static class NumberExtensions
    {
        public static char ToDisplayColumn(this int columnNumber)
            => (char)(65 + columnNumber);

        public static int ToNumberColumn(this char columnChar)
            => columnChar - 65;

        public static string ToDisplayColumnAsString(this int columnNumber)
            => ToDisplayColumn(columnNumber).ToString();

        public static string ToDisplayRow(this int rowNumber)
            => (rowNumber + 1).ToString();
    }
}
