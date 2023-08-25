using System;

namespace LIT.Smabu.Shared.Entities.Business
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

