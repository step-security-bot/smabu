using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.BusinessDomain.Customer
{
    public class BankDetailId : EntityId<IBankDetail>
    {
        public BankDetailId(Guid value) : base(value)
        {
        }
    }
}