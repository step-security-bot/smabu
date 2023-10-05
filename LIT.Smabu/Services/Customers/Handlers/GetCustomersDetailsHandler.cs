using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Shared.Domain.Customers;
using LIT.Smabu.Shared.Domain.Customers.Queries;

namespace LIT.Smabu.Business.Service.Customers.Queries
{
    public class GetCustomersDetailsHandler : RequestHandler<GetCustomerDetailsQuery, GetCustomerDetailsResponse>
    {
        public GetCustomersDetailsHandler(IAggregateStore aggregateStore) : base(aggregateStore)
        {

        }

        public override Task<GetCustomerDetailsResponse> Handle(GetCustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            var customer = this.AggregateStore.Get(request.CustomerId);
            var result = GetCustomerDetailsResponse.Map(customer);
            return Task.FromResult(result);
        }
    }
}
