using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.CustomerAggregate
{
    public record CustomerId(Guid Value) : EntityId<Customer>(Value);
}