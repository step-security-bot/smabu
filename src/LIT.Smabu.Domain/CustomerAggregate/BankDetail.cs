using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.CustomerAggregate
{
    public class BankDetail : Entity<BankDetailId>
    {
        public override BankDetailId Id => throw new NotImplementedException();
    }
}