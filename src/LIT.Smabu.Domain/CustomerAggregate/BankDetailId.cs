using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.CustomerAggregate
{
    public record BankDetailId(Guid Value) : EntityId<BankDetail>(Value);
}