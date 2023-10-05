using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Service.ReadModels;
using LIT.Smabu.Shared.Domain.Common;
using LIT.Smabu.Shared.Domain.Customers;

namespace LIT.Smabu.Service.Business
{
    public class CustomerService
    {
        private readonly IAggregateStore aggregateStore;
        private readonly CustomerReadModel customerReadModel;

        public CustomerService(IAggregateStore aggregateStore, CustomerReadModel customerReadModel)
        {
            this.aggregateStore = aggregateStore;
            this.customerReadModel = customerReadModel;
        }

        public async Task<Customer> CreateAsync(string name)
        {
            var number = CreateNewNumber();
            var customer = Customer.Create(new CustomerId(Guid.NewGuid()), number, name, "");
            await this.aggregateStore.AddOrUpdateAsync(customer);
            return customer;
        }

        public async Task<Customer> EditAsync(CustomerId id, string name, string industryBranch = "")
        {
            var customer = this.aggregateStore.Get<Customer, CustomerId>(id);
            customer.Edit(name, industryBranch);
            await this.aggregateStore.AddOrUpdateAsync(customer);
            return customer;
        }

        public async Task<Customer> EditMainAddressAsync(CustomerId id, string name1, string name2, 
            string street, string houseNumber, string additional, string postalCode, string city, string country)
        {
            var customer = this.aggregateStore.Get<Customer, CustomerId>(id);
            customer.EditAddress(new Address(name1, name2, street, houseNumber, additional, postalCode, city, country));
            await this.aggregateStore.AddOrUpdateAsync(customer);
            return customer;
        }

        private CustomerNumber CreateNewNumber()
        {
            var lastNumber = customerReadModel.GetOverview()
                .Select(x => x.Number)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            return lastNumber == null ? CustomerNumber.CreateFirst() : CustomerNumber.CreateNext(lastNumber);
        }
    }
}
