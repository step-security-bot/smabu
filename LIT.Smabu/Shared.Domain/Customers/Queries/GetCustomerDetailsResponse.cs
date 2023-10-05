using LIT.Smabu.Shared.Domain.Customers;

namespace LIT.Smabu.Shared.Domain.Customers.Queries
{
    public class GetCustomerDetailsResponse
    {

        public GetCustomerDetailsResponse(CustomerId id, string name, string industryBranch)
        {
            Id = id;
            Name = name;
            IndustryBranch = industryBranch;
        }

        public CustomerId Id { get; set; }
        public string Name { get; set; }
        public string IndustryBranch { get; set; }

        public static GetCustomerDetailsResponse Map(Customer customer)
        {
            return new GetCustomerDetailsResponse(customer.Id, customer.Name, customer.IndustryBranch);
        }
    }
}
