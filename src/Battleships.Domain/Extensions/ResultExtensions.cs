namespace Battleships.Domain.Extensions
{
    using Battleships.Domain.Common;
    using System;

    public static class ResultExtensions
    {
        public static Result<T> OnSuccess<T>(this Result<T> result, Action action)
        {
            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }

        public static Result<T> OnFailure<T>(this Result<T> result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }
    }
}
