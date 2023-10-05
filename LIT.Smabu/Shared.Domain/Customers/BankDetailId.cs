using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Shared.Domain.Customers
{
    public class BankDetailId : EntityId<BankDetail>
    {
        public BankDetailId(Guid value) : base(value)
        {
        }
    }
}