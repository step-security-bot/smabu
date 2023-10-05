using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Shared.Domain.Business.CustomerAggregate
{
    public class BankDetailId : EntityId<BankDetail>
    {
        public BankDetailId(Guid value) : base(value)
        {
        }
    }
}