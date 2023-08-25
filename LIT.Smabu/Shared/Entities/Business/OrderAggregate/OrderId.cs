namespace LIT.Smabu.Shared.Entities.Business.OrderAggregate
{
    public class OrderId : EntityId<IOrder>
    {
        public OrderId(Guid value) : base(value)
        {
        }
    }
}