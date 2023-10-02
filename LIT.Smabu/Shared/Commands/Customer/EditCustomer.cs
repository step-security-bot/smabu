using LIT.Smabu.Shared.Entities.Business.CustomerAggregate;

namespace LIT.Smabu.Shared.Commands.Customer
{
    public class EditCustomer
    {
        public required CustomerId Id { get; set; }
        public required string Name { get; set; }
        public string? IndustryBranch { get; set; }
    }
}
