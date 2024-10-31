using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.Common
{
    public record Quantity(decimal Value, Unit Unit) : IValueObject
    {
        public static Quantity Empty() => new(0, Unit.None);

        public override string ToString() => $"{Value} {Unit.Value}";
    }
}