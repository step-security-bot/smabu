using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.CustomerAggregate
{
    public record CustomerId(Guid Value) : EntityId<Customer>(Value);
}