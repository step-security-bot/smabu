using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Shared.Domain.Business.OrderAggregate
{
    public class Order : Entity<OrderId>
    {
        public override OrderId Id => throw new NotImplementedException();
    }
}

