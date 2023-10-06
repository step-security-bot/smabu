using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Orders
{
    public class Order : Entity<OrderId>
    {
        public override OrderId Id => throw new NotImplementedException();
    }
}

