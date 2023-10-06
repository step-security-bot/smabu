using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Products
{
    public class ProductId : EntityId<Product>
    {
        public ProductId(Guid value) : base(value)
        {
        }
    }
}