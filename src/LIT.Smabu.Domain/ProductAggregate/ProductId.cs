using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.ProductAggregate
{
    public class ProductId : EntityId<Product>
    {
        public ProductId(Guid value) : base(value)
        {
        }
    }
}