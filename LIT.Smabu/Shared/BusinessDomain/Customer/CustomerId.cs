using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.BusinessDomain.Customer
{
    public class CustomerId : EntityId<ICustomer>
    {
        public CustomerId(Guid value) : base(value)
        {
        }
    }
}