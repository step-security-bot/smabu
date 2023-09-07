using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.Entities
{
    public class SimpleValueObject<T> : IValueObject
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

        public override int GetHashCode()
        {
            if (Value is not null)
            {
                return Value.GetHashCode();
            }
            else
            {
                throw new ArgumentException("Wert darf nicht NULL sein.");
            }
        }

        public override string ToString()
        {
            return Value?.ToString() ?? "";
        }
    }
}