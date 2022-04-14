namespace Battleships.Domain.Common
{
    internal class Result
    {
        internal Result()
        {
            IsSuccess = true;
            ErrorMessage = string.Empty;
        }

        internal Result(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }

        internal bool IsSuccess { get; }

        internal bool IsFailure => !IsSuccess;

        internal string ErrorMessage { get; }

        internal static Result Success() => new();

        internal static Result<T> Success<T>(T data) => new(data);

        internal static Result Failure(string errorMessage) => new(errorMessage);

        internal static Result<T> Failure<T>(string errorMessage) => new(errorMessage);
    }

    internal class Result<T> : Result
    {
        internal Result(T data) => Data = data;

        internal Result(string errorMessage) : base(errorMessage) => Data = default;

        internal T? Data { get; }
    }
}
