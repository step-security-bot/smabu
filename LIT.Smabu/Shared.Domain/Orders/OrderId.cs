using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Orders
{
    public class OrderId : EntityId<Order>
    {
        public OrderId(Guid value) : base(value)
        {
        }
    }
}