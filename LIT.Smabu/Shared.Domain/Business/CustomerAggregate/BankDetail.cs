using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Shared.Domain.Business.CustomerAggregate
{
    public class BankDetail : Entity<BankDetailId>
    {
        public override BankDetailId Id => throw new NotImplementedException();
    }
}