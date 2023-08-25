namespace LIT.Smabu.Shared.Entities.Business.CustomerAggregate
{
    public class CustomerId : EntityId<Customer>
    {
        public CustomerId(Guid value) : base(value)
        {
        }
    }
}