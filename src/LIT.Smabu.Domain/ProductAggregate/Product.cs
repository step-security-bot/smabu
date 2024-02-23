using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.ProductAggregate
{
    public class Product : Entity<ProductId>
    {
        public override ProductId Id => throw new NotImplementedException();
    }
}
