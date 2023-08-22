using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.BusinessDomain.Customer
{
    public class CustomerId : EntityId<Customer>
    {
        public CustomerId(Guid value) : base(value)
        {
        }
    }
}