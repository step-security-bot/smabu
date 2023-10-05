using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Shared.Domain.Business.OrderAggregate
{
    public class OrderId : EntityId<Order>
    {
        public OrderId(Guid value) : base(value)
        {
        }
    }
}