using LIT.Smabu.Shared.Domain.Common;
using MediatR;

namespace LIT.Smabu.Shared.Domain.Customers.Commands
{
    public class EditCustomerCommand : IRequest<CustomerId>
    {
        public required CustomerId Id { get; set; }
        public required string Name { get; set; }
        public string? IndustryBranch { get; set; }
        public Address? MainAddress { get; set; }
        public Communication? Communication { get; set; }
    }
}
