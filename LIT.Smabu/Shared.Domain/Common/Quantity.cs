using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Shared.Domain.Common
{
    public class Quantity : IValueObject
    {
        public Quantity(decimal value, string unit)
        {
            Value = value;
            Unit = unit;
        }

        public decimal Value { get; }
        public string Unit { get; }
    }
}

