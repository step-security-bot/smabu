using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Customers.Create
{
    public record CreateCustomerCommand : ICommand<CustomerId>
    {
        public required CustomerId CustomerId { get; set; }
        public required string Name { get; set; }
        public CustomerNumber? Number { get; set; }
    }
}
