using LIT.Smabu.Domain.Shared.Customers.Queries;
using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;

namespace LIT.Smabu.Business.Service.Customers.Queries
{
    public class GetCustomersDetailsHandler : RequestHandler<GetCustomerDetailsQuery, GetCustomerDetailsResponse>
    {
        public GetCustomersDetailsHandler(IAggregateStore aggregateStore) : base(aggregateStore)
        {

        }

        public override async Task<GetCustomerDetailsResponse> Handle(GetCustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            var customer = await this.AggregateStore.GetAsync(request.CustomerId);
            var result = GetCustomerDetailsResponse.Map(customer);
            return result;
        }
    }
}
