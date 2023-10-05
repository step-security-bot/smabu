using LIT.Smabu.Shared.Domain.Common;
using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Shared.Domain.Customers
{
    public class Customer : AggregateRoot<CustomerId>
    {
        public Customer(CustomerId id, CustomerNumber number, string name, string industryBranch,
            Address mainAddress, Communication communication)
        {
            Id = id;
            Number = number;
            Name = name;
            IndustryBranch = industryBranch;
            MainAddress = mainAddress;
            Communication = communication;
        }

        public override CustomerId Id { get; }
        public CustomerNumber Number { get; private set; }
        public string Name { get; private set; }
        public string IndustryBranch { get; private set; }
        public Address MainAddress { get; private set; }
        public Communication Communication { get; private set; }

        public static Customer Create(CustomerId id, CustomerNumber number, string name, string industryBranch)
        {
            return new Customer(id, number, name, industryBranch,
                new Address(name, "", "", "", "", "", "", ""),
                new Communication("", "", "", ""));
        }

        public void Edit(string name, string? industryBranch)
        {
            Name = name;
            IndustryBranch = industryBranch ?? "";
        }

        public void EditAddress(Address mainAddress)
        {
            MainAddress = mainAddress;
        }
    }
}
