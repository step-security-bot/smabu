using LIT.Smabu.Domain.Shared.Common.Dtos;

namespace LIT.Smabu.Domain.Shared.Customers.Queries
{
    public class GetCustomerDetailsResponse
    {

        public GetCustomerDetailsResponse(CustomerId id, string name, string industryBranch, AddressDto mainAddress, CommunicationDto communication)
        {
            Id = id;
            Name = name;
            IndustryBranch = industryBranch;
            MainAddress = mainAddress;
            Communication = communication;
        }

        public CustomerId Id { get; set; }
        public string Name { get; set; }
        public string IndustryBranch { get; set; }
        public AddressDto MainAddress { get; set; }
        public CommunicationDto Communication { get; set; }

        public static GetCustomerDetailsResponse Map(Customer customer)
        {
            return new GetCustomerDetailsResponse(customer.Id, customer.Name, customer.IndustryBranch,
                AddressDto.Map(customer.MainAddress), CommunicationDto.Map(customer.Communication));
        }
    }
}
