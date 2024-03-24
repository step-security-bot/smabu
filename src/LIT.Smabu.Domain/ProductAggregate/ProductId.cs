using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.ProductAggregate
{
    public record ProductId(Guid Value) : EntityId<Product>(Value);
}