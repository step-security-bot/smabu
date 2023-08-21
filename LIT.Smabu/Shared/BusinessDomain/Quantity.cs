using System;
using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.BusinessDomain
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

