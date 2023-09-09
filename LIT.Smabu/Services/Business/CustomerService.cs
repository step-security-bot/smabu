using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Service.ReadModels;
using LIT.Smabu.Shared.Entities.Business.CustomerAggregate;

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

        public async Task<Customer> CreateAsync(string name1, string name2)
        {
            var number = CreateNewNumber();
            var result = Customer.Create(new CustomerId(Guid.NewGuid()), number, name1, name2);
            await this.aggregateStore.AddOrUpdateAsync(result);
            return result;
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
