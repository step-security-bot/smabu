using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Shared.Domain.Customers;
using LIT.Smabu.Shared.Domain.Customers.Queries;

namespace LIT.Smabu.Business.Service.Customers.Queries
{
    public class GetAllCustomersQueryHandler : RequestHandler<GetAllCustomersQuery, GetAllCustomersResponse[]>
    {
        public GetAllCustomersQueryHandler(IAggregateStore aggregateStore) : base(aggregateStore)
        {

        }

        public override Task<GetAllCustomersResponse[]> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = this.AggregateStore.GetAll<Customer>();
            var result = customers
                .Select(x => GetAllCustomersResponse.Map(x))
                .OrderBy(x => x.Name)
                .ToArray();
            return Task.FromResult(result);
        }
    }
}
