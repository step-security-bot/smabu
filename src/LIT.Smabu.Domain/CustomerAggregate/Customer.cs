using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.CustomerAggregate
{
    public class Customer(CustomerId id, CustomerNumber number, string name, string industryBranch,
        Currency currency, Address mainAddress, Communication communication) : AggregateRoot<CustomerId>
    {
        public override CustomerId Id { get; } = id;
        public CustomerNumber Number { get; private set; } = number;
        public string Name { get; private set; } = name;
        public string IndustryBranch { get; private set; } = industryBranch;
        public Currency Currency { get; set; } = currency;
        public Address MainAddress { get; private set; } = mainAddress;
        public Communication Communication { get; private set; } = communication;

        public static Customer Create(CustomerId id, CustomerNumber number, string name, string industryBranch)
        {
            return new Customer(id, number, name, industryBranch, Currency.EUR,
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

        public override Result Delete()
        {
            return base.Delete();
        }
    }
}
