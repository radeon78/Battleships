namespace Battleships.Domain.Extensions
{
    using System;

    public static class BoolExtensions
    {
        public static bool IsTrue(this bool result, Action action)
        {
            if (result) action();
            return result;
        }

        public static bool IsFalse(this bool result, Action action)
        {
            if (!result) action();
            return result;
        }
    }
}
