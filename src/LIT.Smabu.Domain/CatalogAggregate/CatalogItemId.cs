using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.CatalogAggregate
{
    public record CatalogItemId(Guid Value) : EntityId<CatalogItem>(Value);
}