using LIT.Smabu.Business.Service.Mapping;
using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Customers;
using MediatR;

namespace LIT.Smabu.Business.Service.Customers.Queries
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, CustomerDTO[]>
    {
        private readonly IAggregateStore aggregateStore;
        private readonly IMapper mapper;

        public GetCustomersQueryHandler(IAggregateStore aggregateStore, IMapper mapper)
        {
            this.aggregateStore = aggregateStore;
            this.mapper = mapper;
        }

        public async Task<CustomerDTO[]> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await this.aggregateStore.GetAsync<Customer>();
            var result = this.mapper.Map<Customer, CustomerDTO>(customers)
                .OrderBy(x => x.Name)
                .ToArray();
            return result;
        }
    }
}
