namespace LIT.Smabu.Shared.Entities.Business.ProductAggregate
{
    public class ProductId : EntityId<Product>
    {
        public ProductId(Guid value) : base(value)
        {
        }
    }
}