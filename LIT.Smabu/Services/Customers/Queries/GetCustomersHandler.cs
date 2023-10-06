using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Shared.Customers;

namespace LIT.Smabu.Business.Service.Customers.Queries
{
    public class GetCustomersQueryHandler : RequestHandler<GetCustomersQuery, CustomerDTO[]>
    {
        public GetCustomersQueryHandler(IAggregateStore aggregateStore) : base(aggregateStore)
        {

        }

        public override async Task<CustomerDTO[]> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await this.AggregateStore.GetAllAsync<Customer>();
            var result = customers
                .Select(x => CustomerDTO.Map(x))
                .OrderBy(x => x.Name)
                .ToArray();
            return result;
        }
    }
}
