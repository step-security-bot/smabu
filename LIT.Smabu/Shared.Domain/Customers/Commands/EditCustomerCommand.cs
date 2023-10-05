using LIT.Smabu.Shared.Domain.Customers;

namespace LIT.Smabu.Shared.Domain.Customers.Commands
{
    public class EditCustomerCommand
    {
        public required CustomerId Id { get; set; }
        public required string Name { get; set; }
        public string? IndustryBranch { get; set; }
    }
}
