using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs
{
    public record CatalogItemDTO(CatalogItemId Id, CatalogGroupId CatalogGroupId, CatalogId CatalogId, CatalogItemNumber Number, string Name, 
        string Description, bool IsActive, string GroupName,
        CatalogItemPrice[] Prices, Dictionary<CustomerId, CatalogItemPrice> CustomerPrices, Unit Unit, CatalogItemPrice CurrentPrice) : IDTO
    {
        public static CatalogItemDTO Create(CatalogItem item, CatalogGroup catalogGroup)
        {
            return new CatalogItemDTO(item.Id, item.CatalogGroupId, item.CatalogId, item.Number, item.Name, item.Description, item.IsActive, 
                catalogGroup.Name,
                [.. item.Prices], 
                item.CustomerPrices.ToDictionary(x => x.Key, x => x.Value) , item.Unit, item.GetCurrentPrice());
        }

        public string DisplayName => $"{Number.DisplayName} {Name}";
        public Currency Currency { get; } = Currency.EUR;
    }
}