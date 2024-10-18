using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.ProductAggregate
{
    public record ProductId(Guid Value) : EntityId<Product>(Value);
}