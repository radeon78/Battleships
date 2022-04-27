namespace Battleships.Domain.Extensions
{
    using System;

    public static class BoolExtensions
    {
        public static bool WhenTrue(this bool source, Action action)
        {
            if (source) action();
            return source;
        }

        public static bool WhenFalse(this bool source, Action action)
        {
            if (!source) action();
            return source;
        }
    }
}
