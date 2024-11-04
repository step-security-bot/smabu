using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Customers.Update
{
    public record UpdateCustomerCommand : ICommand<CustomerId>
    {
        public required CustomerId CustomerId { get; set; }
        public required string Name { get; set; }
        public required string IndustryBranch { get; set; }
        public Address? MainAddress { get; set; }
        public Communication? Communication { get; set; }
        public CorporateDesign? CorporateDesign { get; set; }
    }
}
