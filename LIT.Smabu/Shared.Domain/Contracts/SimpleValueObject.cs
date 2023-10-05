namespace LIT.Smabu.Shared.Domain.Contracts
{
    public abstract class SimpleValueObject<T> : IValueObject, IComparable
    {
        public SimpleValueObject(T value)
        {
            if (value is null)
            {
                throw new ArgumentException("Wert darf nicht NULL sein.");
            }
            Value = value;
        }

        public T Value { get; }

        public override bool Equals(object? obj)
        {
            if (obj is SimpleValueObject<T> target)
            {
                return Value?.Equals(target.Value) ?? false;
            }
            else
            {
                throw new ArgumentException("Wert darf nicht NULL sein.");
            }
        }

        public override int GetHashCode() => Value is not null ? Value.GetHashCode() : -1;

        public virtual int CompareTo(object? obj) => obj is not null ? ToString().CompareTo(obj.ToString()) : -1;

        public override string ToString() => Value?.ToString() ?? "";
    }
}