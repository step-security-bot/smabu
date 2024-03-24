using LIT.Smabu.Shared.Interfaces;
using System.Globalization;

namespace LIT.Smabu.Domain.Common
{
    public record Currency(string IsoCode, string Name, string Sign) : IValueObject
    {

        public string Format(decimal amount)
        {
            var numberFormatInfo = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
            numberFormatInfo.CurrencySymbol = IsoCode;
            string formattedNumber = amount.ToString("C", numberFormatInfo);
            return formattedNumber;
        }
        public static Currency GetEuro() => new("EUR", "Euro", "€");

        public static Currency[] GetAll() => [GetEuro()];

        public override string ToString() => $"{Name} ({Sign})";

        public override int GetHashCode()
        {
            return IsoCode.GetHashCode();
        }
    }
}

