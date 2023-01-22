namespace Battleships.Domain.Extensions;

public static class EnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action, CancellationToken cancellationToken)
    {
        foreach (var item in enumerable)
        {
            if (cancellationToken.IsCancellationRequested) return;
            action(item);
        }
    }
}