using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.OrderAggregate
{
    public record OrderId(Guid Value) : EntityId<Order>(Value);
}