using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.CatalogAggregate
{
    public record CatalogGroupId(Guid Value) : EntityId<CatalogGroup>(Value);
}