using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.Customers
{
    public class CustomerDTO
    {
        public CustomerId Id { get; set; }
        public CustomerNumber Number { get; set; }
        public string Name { get; set; }
        public string IndustryBranch { get; set; }
        public AddressDTO MainAddress { get; set; }
        public CommunicationDTO Communication { get; set; }
        public decimal TotalSales { get; set; }
    }
}
