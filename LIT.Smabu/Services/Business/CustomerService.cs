using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Service.ReadModels;

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
    }
}
