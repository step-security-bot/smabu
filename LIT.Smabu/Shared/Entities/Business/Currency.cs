using System;

namespace LIT.Smabu.Shared.Entities.Business
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

