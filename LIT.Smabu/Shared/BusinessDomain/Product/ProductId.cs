using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.BusinessDomain.Product
{
    public class ProductId : EntityId<Product>
    {
        public ProductId(Guid value) : base(value)
        {
        }
    }
}