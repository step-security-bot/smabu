namespace LIT.Smabu.Domain.SeedWork
{
    public abstract record SimpleValueObject<T> : IValueObject, IComparable
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
        public virtual int CompareTo(object? obj) => obj is not null ? ToString().CompareTo(obj.ToString()) : -1;
        public override string ToString() => Value!.ToString() ?? "";
    }
}