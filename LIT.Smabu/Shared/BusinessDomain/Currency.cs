using System;
using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.BusinessDomain
{
    public class Currency : IValueObject
    {
        public Currency(string isoCode, string name, string sign)
        {
            IsoCode = isoCode;
            Name = name;
            Sign = sign;
        }

        public string IsoCode { get; }
        public string Name { get; }
        public string Sign { get; }

        public static Currency GetEuro() => new("EUR", "Euro", "€");
    }
}

