using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.OrderAggregate
{
    public class OrderId(Guid value) : EntityId<Order>(value);
}