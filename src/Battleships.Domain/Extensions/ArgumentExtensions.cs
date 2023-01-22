using System.Text.RegularExpressions;

namespace Battleships.Domain.Extensions;

public static class ArgumentExtensions
{
    public static string NonEmpty(this string value, string name)
        => !string.IsNullOrEmpty(value) ? value : throw new ArgumentNullException(name);

    public static string PatternMatch(this string value, string pattern, string name)
        => Regex.Match(value, pattern).Success ? value : throw new ArgumentException(value, name);

    public static int InRange(this int value, int min, int max, string name)
        => value < min || value > max ? throw new ArgumentException(name) : value;

    public static T NonNull<T>(this T value, string name)
        => value == null ? throw new ArgumentNullException(name) : value;
}