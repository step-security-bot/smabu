using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.ProductAggregate
{
    public class Product : Entity<ProductId>
    {
        public override ProductId Id => throw new NotImplementedException();
    }
}
