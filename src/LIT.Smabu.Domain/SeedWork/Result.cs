namespace LIT.Smabu.Domain.SeedWork
{
    public sealed class Result<TValue> : Result
    {
        private Result(TValue? value, Error error)
            : base(error == Error.None, error)
        {
            Value = value;
        }

        public TValue? Value { get; }

        public override bool HasReturnValue => Value != null;

        public override object? GetValue() => Value;

        public static Result<TValue> Success(TValue value) => new(value, Error.None);

        public static new Result<TValue> Failure(Error error) => new(default, error);

        public static implicit operator Result<TValue>(TValue value) => new(value, Error.None);

        public static implicit operator Result<TValue>(Error error) => new(default, error);
    }

    public class Result
    {
        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        public virtual bool HasReturnValue => false;

        public virtual object? GetValue() => null;

        public static Result Success() => new(true, Error.None);

        public static Result Failure(Error error) => new(false, error);

        public static implicit operator Result(bool value) => new(value, Error.None);

        public static implicit operator Result(Error error) => new(false, error);
    }

    public sealed record Error(string Code, string Description)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
    }
}
