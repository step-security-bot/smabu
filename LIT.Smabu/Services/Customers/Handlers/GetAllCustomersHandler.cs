using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Domain.Shared.Customers.Queries;
using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;

namespace LIT.Smabu.Business.Service.Customers.Queries
{
    public class GetAllCustomersQueryHandler : RequestHandler<GetAllCustomersQuery, GetAllCustomersResponse[]>
    {
        public GetAllCustomersQueryHandler(IAggregateStore aggregateStore) : base(aggregateStore)
        {

        }

        public override async Task<GetAllCustomersResponse[]> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await this.AggregateStore.GetAllAsync<Customer>();
            var result = customers
                .Select(x => GetAllCustomersResponse.Map(x))
                .OrderBy(x => x.Name)
                .ToArray();
            return result;
        }
    }
}
