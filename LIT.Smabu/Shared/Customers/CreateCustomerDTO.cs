using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Shared.Contracts;

namespace LIT.Smabu.Shared.Customers
{
    public record CreateCustomerDTO : IDTO
    {
        public CustomerId Id { get; set; }
        public string Name { get; set; }
    }
}
