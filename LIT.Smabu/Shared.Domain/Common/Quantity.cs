using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Common
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

