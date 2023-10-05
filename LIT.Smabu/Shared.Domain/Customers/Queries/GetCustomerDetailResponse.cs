using LIT.Smabu.Shared.Domain.Customers;

namespace LIT.Smabu.Shared.Domain.Customers.Queries
{
    public class GetCustomerDetailResponse
    {
        public GetCustomerDetailResponse(CustomerId id, string name, string industryBranch)
        {
            Id = id;
            Name = name;
            IndustryBranch = industryBranch;
        }

        public CustomerId Id { get; set; }
        public string Name { get; set; }
        public string IndustryBranch { get; set; }

        public static GetCustomerDetailResponse From(Customer customer)
        {
            return new GetCustomerDetailResponse(customer.Id, customer.Name, customer.IndustryBranch);
        }
    }
}
