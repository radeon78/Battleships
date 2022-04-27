namespace Battleships.Domain.Common;

public static class RegexPatterns
{
    public static string PointPattern => "^[A-Ja-j]([1-9]|10)$";

    public static string DirectionPattern => "^[h|H|v|V]$";
}