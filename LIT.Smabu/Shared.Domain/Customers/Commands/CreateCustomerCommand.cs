using MediatR;

namespace LIT.Smabu.Domain.Shared.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<CustomerId>
    {
        public required CustomerId Id { get; set; }
        public required string Name { get; set; }
        public CustomerNumber? Number { get; set; }
    }
}
