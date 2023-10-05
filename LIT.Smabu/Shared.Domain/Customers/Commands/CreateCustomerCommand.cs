using MediatR;

namespace LIT.Smabu.Shared.Domain.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<CustomerId>
    {
        public required CustomerId Id { get; set; }
        public required string Name { get; set; }
        public CustomerNumber? Number { get; set; }
    }
}
