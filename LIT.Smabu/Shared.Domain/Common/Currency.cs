using LIT.Smabu.Domain.Shared.Contracts;
using System.Globalization;

namespace LIT.Smabu.Domain.Shared.Common
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

        public string Format(decimal amount)
        {
            var numberFormatInfo = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
            numberFormatInfo.CurrencySymbol = IsoCode;
            string formattedNumber = amount.ToString("C", numberFormatInfo);
            return formattedNumber;
        }
        public static Currency GetEuro() => new("EUR", "Euro", "€");

        public static Currency[] GetAll() => new Currency[] { GetEuro() };


        public override int GetHashCode()
        {
            return IsoCode.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return IsoCode.Equals((obj as Currency)?.IsoCode);
        }
    }
}

