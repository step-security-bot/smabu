using LIT.Smabu.Shared.Domain.Business.CustomerAggregate;

namespace LIT.Smabu.Shared.Customers
{
    public class CustomerOverviewDto
    {
        public CustomerOverviewDto(CustomerId id, CustomerNumber number, string name, string industryBranch, string city)
        {
            Id = id;
            Number = number;
            Name = name;
            IndustryBranch = industryBranch;
            City = city;
        }

        public CustomerId Id { get; }
        public CustomerNumber Number { get; }
        public string Name { get; }
        public string IndustryBranch { get; }
        public string City { get; }

        public static CustomerOverviewDto From(Customer customer)
        {
            return new CustomerOverviewDto(customer.Id, customer.Number, customer.Name, customer.IndustryBranch,
                customer.MainAddress.City);
        }
    }
}
