using LIT.Smabu.Shared.Domain.Business.CustomerAggregate;

namespace LIT.Smabu.Shared.Customers
{
    public class EditCustomerRequest
    {
        public required CustomerId Id { get; set; }
        public required string Name { get; set; }
        public string? IndustryBranch { get; set; }
    }
}
