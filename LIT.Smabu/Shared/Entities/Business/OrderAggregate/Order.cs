using System;

namespace LIT.Smabu.Shared.Entities.Business.OrderAggregate
{
    public class Order : Entity<OrderId>
    {
        public override OrderId Id => throw new NotImplementedException();
    }
}

