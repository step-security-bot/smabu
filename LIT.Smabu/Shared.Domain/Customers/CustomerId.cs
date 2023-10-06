using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Customers
{
    public class CustomerId : EntityId<Customer>
    {
        public CustomerId(Guid value) : base(value)
        {
        }
    }
}