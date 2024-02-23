using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.CustomerAggregate
{
    public class CustomerId(Guid value) : EntityId<Customer>(value)
    {
    }
}