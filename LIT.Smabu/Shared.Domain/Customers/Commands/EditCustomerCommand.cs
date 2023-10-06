using LIT.Smabu.Domain.Shared.Common;
using MediatR;

namespace LIT.Smabu.Domain.Shared.Customers.Commands
{
    public record EditCustomerCommand : IRequest<CustomerId>
    {
        public required CustomerId Id { get; set; }
        public required string Name { get; set; }
        public required string IndustryBranch { get; set; }
        public Address? MainAddress { get; set; }
        public Communication? Communication { get; set; }
    }
}
