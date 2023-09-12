namespace LIT.Smabu.Shared.Entities.Business.OrderAggregate
{
    public class OrderId : EntityId<Order>
    {
        public OrderId(Guid value) : base(value)
        {
        }
    }
}