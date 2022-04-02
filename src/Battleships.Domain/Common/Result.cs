namespace Battleships.Domain.Common
{
    public class Result
    {
        public Result()
        {
            IsSuccess = true;
            ErrorMessage = string.Empty;
        }

        public Result(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public string ErrorMessage { get; }

        public static Result Success() => new();

        public static Result<T> Success<T>(T data) => new(data);

        public static Result Failure(string errorMessage) => new(errorMessage);

        public static Result<T> Failure<T>(string errorMessage) => new(errorMessage);
    }

    public class Result<T> : Result
    {
        public Result(T data) => Data = data;

        public Result(string errorMessage) : base(errorMessage) => Data = default;

        public T? Data { get; }
    }
}
