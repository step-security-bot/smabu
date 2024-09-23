namespace LIT.Smabu.Domain.SeedWork
{
    public class Result<TValue> : Result
    {
        private Result(TValue value, Error error)
            : base(true, error)
        {
            this.Value = value;
        }

        public TValue Value { get; }

        public static Result<TValue> Success(TValue value) => new(value, Error.None);

        public static implicit operator Result<TValue>(TValue value) => new(value, Error.None);
    }

    public class Result
    {
        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        public static Result Success() => new(true, Error.None);

        public static Result Failure(Error error) => new(false, error);

        public static implicit operator Result(bool value) => new(value, Error.None);

        public static implicit operator Result(Error error) => new(false, error);
    }

    public sealed record Error(string Code, string Description)
    {
        public static readonly Error None = new(string.Empty, string.Empty);

        internal static Error HasReferences(string code, string[] types)
        {
            var filteredTypes = types.Where(t => !string.IsNullOrEmpty(t)).ToArray();
            return new Error(code, $"Es sind bereits {string.Join(" und ", filteredTypes)} verknüpft.");
        }
    }
}
