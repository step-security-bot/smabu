using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.CatalogAggregate
{
    public class CatalogItem : AggregateRoot<CatalogItemId>
    {
        private readonly List<CatalogItemPrice> _prices;
        private readonly Dictionary<CustomerId, CatalogItemPrice> _customerPrices;

        public CatalogItem(CatalogItemId id, CatalogItemNumber number, CatalogGroupId catalogGroupId, bool isActive,
            string name, string description, Unit unit,
            List<CatalogItemPrice>? prices,
            Dictionary<CustomerId, CatalogItemPrice>? customerPrices)
        {
            Id = id;
            Number = number;
            CatalogGroupId = catalogGroupId;
            IsActive = isActive;
            Name = name;
            Description = description;
            Unit = unit;
            _prices = prices ?? [];
            _customerPrices = customerPrices ?? [];
        }

        public static CatalogItem Create(CatalogItemId id, CatalogItemNumber number, CatalogGroupId catalogGroupId,
            string name, string description, Unit unit)
        {
            var defaultPrices = new List<CatalogItemPrice>()
            {
                CatalogItemPrice.Create(0)
            };
            return new CatalogItem(id, number, catalogGroupId, true, name, description, unit, defaultPrices, null);
        }

        public override CatalogItemId Id { get; }
        public CatalogItemNumber Number { get; private set; }
        public CatalogGroupId CatalogGroupId { get; private set; }

        public bool IsActive { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Unit Unit { get; private set; }

        public IReadOnlyList<CatalogItemPrice> Prices => _prices;
        public IReadOnlyDictionary<CustomerId, CatalogItemPrice> CustomerPrices => _customerPrices;

        public CatalogItemPrice GetCurrentPrice() => Prices
            .Where(p => p.CheckIsValidToday())
            .OrderByDescending(p => p.ValidFrom)
            .FirstOrDefault(CatalogItemPrice.Empty)!;

        public CatalogItemPrice GetCurrentPrice(CustomerId customerId) => CustomerPrices.ContainsKey(customerId)
            ? CustomerPrices[customerId]
            : GetCurrentPrice();

        public Result Update(bool isActive, string name, string description, Unit unit)
        {
            List<Error> validationErrors = Validate(name, unit);
            if (validationErrors.Count != 0)
            {
                return Result.Failure(validationErrors);
            }

            IsActive = isActive;
            Name = name;
            Description = description;
            Unit = unit;

            return Result.Success();
        }

        public Result UpdatePrices(CatalogItemPrice[] prices)
        {
            if (prices?.Length == 0 || prices?.Where(x => x.CheckIsValidToday()).Count() == 0)
            {
                return CatalogErrors.NoValidPrice;
            }

            _prices.Clear();
            _prices.AddRange(prices!.OrderByDescending(x => x.ValidFrom));

            return Result.Success();
        }

        private static List<Error> Validate(string name, Unit unit)
        {
            var validationErrors = new List<Error>();
            if (string.IsNullOrEmpty(name))
            {
                validationErrors.Add(CatalogErrors.NameEmpty);
            }
            if (unit == null)
            {
                validationErrors.Add(CatalogErrors.UnitEmpty);
            }

            return validationErrors;
        }
    }
}
