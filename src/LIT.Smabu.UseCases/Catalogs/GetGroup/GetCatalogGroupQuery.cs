using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs.GetGroup
{
    public record GetCatalogGroupQuery(CatalogGroupId CatalogGroupId, CatalogId CatalogId) : IQuery<CatalogGroupDTO>
    {

    }
}