namespace LIT.Smabu.Shared
{
    public sealed class Result<TValue> : Result
    {
        private Result(TValue? value)
            : base(value)
        {
            Value = value;
        }

        private Result(Error error)
            : base(error)
        {
            Value = default;
        }

        public new TValue? Value { get; }

        public static Result<TValue> Success(TValue value) => new(value);
        public static new Result<TValue> Failure(Error error) => new(error);

        public static implicit operator Result<TValue>(TValue value) => new(value);

        public static implicit operator Result<TValue>(Error error) => new(error);
    }

    public class Result
    {
        protected Result(object? value = null)
        {
            Value = value;
            Error = value is Error error ? error : Error.None;
            IsSuccess = Error == Error.None;
        }

        public object? Value { get; }

        public Error Error { get; }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public static Result Success() => new();

        public static Result Failure(Error error) => new(error);

        //public static implicit operator Result(object value) => new(value);

        public static implicit operator Result(Error error) => new(error);
    }

    public sealed record Error(string Code, string Description)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
    }
}
