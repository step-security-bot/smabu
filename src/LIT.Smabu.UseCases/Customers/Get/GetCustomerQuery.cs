using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Customers.Get
{
    public record GetCustomerQuery(CustomerId CustomerId) : IQuery<CustomerDTO>
    {
    }
}
