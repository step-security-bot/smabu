using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Customers
{
    public class BankDetailId : EntityId<BankDetail>
    {
        public BankDetailId(Guid value) : base(value)
        {
        }
    }
}