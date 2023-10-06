using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Customers;
using MediatR;

namespace LIT.Smabu.Business.Service.Customers.Queries
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, CustomerDTO[]>
    {
        private readonly IAggregateStore aggregateStore;

        public GetCustomersQueryHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<CustomerDTO[]> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await this.aggregateStore.GetAsync<Customer>();
            var result = customers
                .Select(x => CustomerDTO.Map(x))
                .OrderBy(x => x.Name)
                .ToArray();
            return result;
        }
    }
}
