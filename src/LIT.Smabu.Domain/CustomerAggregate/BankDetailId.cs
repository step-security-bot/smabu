using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.CustomerAggregate
{
    public record BankDetailId(Guid Value) : EntityId<BankDetail>(Value);
}