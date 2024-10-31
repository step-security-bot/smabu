using LIT.Smabu.Domain.Common;
using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public record CatalogItemPrice : IValueObject
    {
        public CatalogItemPrice(decimal price, DateTime validFrom)
        {
            Price = price;
            ValidFrom = validFrom;
        }

        public static CatalogItemPrice Create(int price) => new(price, DateTime.Now.Date);
        public static CatalogItemPrice? Empty => new(0, DateTime.Now.Date);

        public decimal Price { get; set; }
        public Currency Currency { get; } = Currency.EUR;
        public DateTime ValidFrom { get; set; }

        public bool CheckIsValidToday() => ValidFrom.Date <= DateTime.Now.Date;
    }
}
