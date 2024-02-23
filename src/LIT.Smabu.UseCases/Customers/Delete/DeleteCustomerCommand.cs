using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Customers.Delete
{
    public record DeleteCustomerCommand : ICommand<bool>
    {
        public required CustomerId Id { get; set; }
    }
}
