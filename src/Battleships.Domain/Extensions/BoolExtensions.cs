namespace Battleships.Domain.Extensions
{
    using System;

    public static class BoolExtensions
    {
        public static bool IfTrue(this bool source, Action action)
        {
            if (source) action();
            return source;
        }

        public static bool IfFalse(this bool source, Action action)
        {
            if (!source) action();
            return source;
        }
    }
}
