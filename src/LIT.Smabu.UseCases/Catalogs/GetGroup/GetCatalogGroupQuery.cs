using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs.GetGroup
{
    public record GetCatalogGroupQuery(CatalogId CatalogId, CatalogGroupId Id) : IQuery<CatalogGroupDTO>
    {

    }
}