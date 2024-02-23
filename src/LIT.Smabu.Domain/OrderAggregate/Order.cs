using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.OrderAggregate
{
    public class Order : Entity<OrderId>
    {
        public override OrderId Id => throw new NotImplementedException();
    }
}

