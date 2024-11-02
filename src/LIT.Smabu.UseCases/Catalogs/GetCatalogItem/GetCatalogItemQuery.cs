using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs.GetCatalogItem
{
    public record GetCatalogItemQuery(CatalogItemId Id, CatalogId CatalogId) : IQuery<CatalogItemDTO>
    {

    }
}