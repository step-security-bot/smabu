using LIT.Smabu.Shared.Domain.Business.CustomerAggregate;

namespace LIT.Smabu.Shared.Customers
{
    public class CustomerDetailDto
    {
        public CustomerDetailDto(CustomerId id, string name, string industryBranch)
        {
            Id = id;
            Name = name;
            IndustryBranch = industryBranch;
        }

        public CustomerId Id { get; set; }
        public string Name { get; set; }
        public string IndustryBranch { get; set; }

        public static CustomerDetailDto From(Customer customer)
        {
            return new CustomerDetailDto(customer.Id, customer.Name, customer.IndustryBranch);
        }
    }
}
