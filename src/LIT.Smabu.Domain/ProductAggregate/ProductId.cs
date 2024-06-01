using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.ProductAggregate
{
    public record ProductId(Guid Value) : EntityId<Product>(Value);
}