using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Customers.List
{
    public class ListCustomersHandler : IQueryHandler<ListCustomersQuery, CustomerDTO[]>
    {
        private readonly IAggregateStore aggregateStore;

        public ListCustomersHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<CustomerDTO[]> Handle(ListCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await aggregateStore.GetAllAsync<Customer>();
            var result = customers.Select(x => CustomerDTO.CreateFrom(x))
                .OrderBy(x => x.Name)
                .ToArray();
            return result;
        }
    }
}
