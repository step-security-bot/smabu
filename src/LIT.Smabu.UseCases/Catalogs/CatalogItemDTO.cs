using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs
{
    public record CatalogItemDTO(CatalogItemId Id, CatalogId CatalogId, CatalogItemNumber Number, string Name, 
        string Description, bool IsActive,
        CatalogItemPrice[] Prices, Dictionary<CustomerId, CatalogItemPrice> CustomerPrices, Unit Unit, CatalogItemPrice CatalogItemPrice) : IDTO
    {
        public static CatalogItemDTO Create(CatalogItem item)
        {
            return new CatalogItemDTO(item.Id, item.CatalogId, item.Number, item.Name, item.Description, item.IsActive, [.. item.Prices], 
                item.CustomerPrices.ToDictionary(x => x.Key, x => x.Value) , item.Unit, item.GetCurrentPrice());
        }

        public string DisplayName => Number.Long;
    }
}