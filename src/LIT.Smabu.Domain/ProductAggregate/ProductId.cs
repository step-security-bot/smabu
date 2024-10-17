using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.ProductAggregate
{
    public record ProductId(Guid Value) : EntityId<Product>(Value);
}