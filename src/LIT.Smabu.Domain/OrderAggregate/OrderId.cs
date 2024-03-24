using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.OrderAggregate
{
    public record OrderId(Guid Value) : EntityId<Order>(Value);
}