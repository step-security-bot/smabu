namespace LIT.Smabu.Shared.Domain.Customers.Queries
{
    public class GetAllCustomersResponse
    {
        public GetAllCustomersResponse(CustomerId id, CustomerNumber number, string name, string industryBranch, string city)
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

        public static GetAllCustomersResponse Map(Customer customer)
        {
            return new GetAllCustomersResponse(customer.Id, customer.Number, customer.Name, customer.IndustryBranch,
                customer.MainAddress.City);
        }
    }
}
