using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.CustomerAggregate
{
    public record BankDetailId(Guid Value) : EntityId<BankDetail>(Value);
}