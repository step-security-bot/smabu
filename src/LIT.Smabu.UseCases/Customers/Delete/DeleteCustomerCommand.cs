using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Customers.Delete
{
    public record DeleteCustomerCommand(CustomerId Id) : ICommand
    {

    }
}
