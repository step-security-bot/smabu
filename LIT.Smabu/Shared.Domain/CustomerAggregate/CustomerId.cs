using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Shared.Domain.CustomerAggregate
{
    public class CustomerId : EntityId<Customer>
    {
        public CustomerId(Guid value) : base(value)
        {
        }
    }
}