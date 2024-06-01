using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.Common
{
    public record Quantity(decimal Value, string Unit) : IValueObject
    {
        public static Quantity Empty() => new(0, "");

        public static string[] GetUnits()
        {
            return ["STD", "STK"];
        }

        public override string ToString() => $"{Value} {Unit}";
    }
}

