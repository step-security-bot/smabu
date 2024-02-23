using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.CustomerAggregate
{
    public class Customer : AggregateRoot<CustomerId>
    {
        public Customer(CustomerId id, CustomerNumber number, string name, string industryBranch,
            Currency currency, Address mainAddress, Communication communication)
        {
            Id = id;
            Number = number;
            Name = name;
            IndustryBranch = industryBranch;
            Currency = currency;
            MainAddress = mainAddress;
            Communication = communication;
        }

        public override CustomerId Id { get; }
        public CustomerNumber Number { get; private set; }
        public string Name { get; private set; }
        public string IndustryBranch { get; private set; }
        public Currency Currency { get; set; }
        public Address MainAddress { get; private set; }
        public Communication Communication { get; private set; }

        public static Customer Create(CustomerId id, CustomerNumber number, string name, string industryBranch)
        {
            return new Customer(id, number, name, industryBranch, Currency.GetEuro(),
                new Address(name, "", "", "", "", "", ""),
                new Communication("", "", "", ""));
        }

        public void Update(string name, string? industryBranch, Address? mainAddress, Communication? communication)
        {
            Name = name;
            IndustryBranch = industryBranch ?? "";
            if (mainAddress != null)
            {
                MainAddress = mainAddress;
            }
            if (communication != null)
            {
                Communication = communication;
            }
        }

        public void Delete()
        {

        }
    }
}
