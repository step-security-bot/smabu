using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.OrderAggregate
{
    public record OrderId(Guid Value) : EntityId<Order>(Value);
}