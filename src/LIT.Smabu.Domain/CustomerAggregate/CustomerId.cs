using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.CustomerAggregate
{
    public record CustomerId(Guid Value) : EntityId<Customer>(Value);
}