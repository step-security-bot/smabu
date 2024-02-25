using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.Domain.Common
{
    public record Quantity : IValueObject
    {
        public Quantity(decimal value, string unit)
        {
            Value = value;
            Unit = unit;
        }

        public decimal Value { get; }
        public string Unit { get; }

        public static Quantity Empty() => new(0, "");

        public static string[] GetUnits()
        {
            return ["STD", "STK"];
        }

        public override string ToString() => $"{Value} {Unit}";
    }
}

