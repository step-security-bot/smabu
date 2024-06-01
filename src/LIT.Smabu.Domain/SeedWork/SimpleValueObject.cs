namespace LIT.Smabu.Domain.SeedWork
{
    public abstract record SimpleValueObject<T> : IValueObject
    {
        public SimpleValueObject(T value)
        {
            if (value is null)
            {
                throw new ArgumentException("Wert darf nicht NULL sein.");
            }
            Value = value;
        }

        public T Value { get; init; }

        public override int GetHashCode() => Value!.GetHashCode();

        public override string ToString() => Value!.ToString() ?? "";
    }
}