using LIT.Smabu.Domain.Shared.Customers;
using MediatR;

namespace LIT.Smabu.Business.Service.Customers.Commands
{
    public record CreateCustomerCommand : IRequest<CustomerId>
    {
        public required CustomerId Id { get; set; }
        public required string Name { get; set; }
        public CustomerNumber? Number { get; set; }
    }
}
