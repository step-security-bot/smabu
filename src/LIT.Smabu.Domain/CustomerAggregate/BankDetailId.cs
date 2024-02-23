using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.CustomerAggregate
{
    public class BankDetailId(Guid value) : EntityId<BankDetail>(value);
}