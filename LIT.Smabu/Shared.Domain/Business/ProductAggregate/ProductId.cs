using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Shared.Domain.Business.ProductAggregate
{
    public class ProductId : EntityId<Product>
    {
        public ProductId(Guid value) : base(value)
        {
        }
    }
}