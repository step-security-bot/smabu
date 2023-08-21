using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.BusinessDomain.Order
{
    public class OrderId : EntityId<IOrder>
    {
        public OrderId(Guid value) : base(value)
        {
        }
    }
}