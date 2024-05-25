using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.CustomerAggregate
{
    public record BankDetailId(Guid Value) : EntityId<BankDetail>(Value);
}