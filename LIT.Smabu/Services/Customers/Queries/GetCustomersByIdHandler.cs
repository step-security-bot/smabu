using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Shared.Customers;

namespace LIT.Smabu.Business.Service.Customers.Queries
{
    public class GetCustomersByIdHandler : RequestHandler<GetCustomerByIdQuery, CustomerDTO>
    {
        public GetCustomersByIdHandler(IAggregateStore aggregateStore) : base(aggregateStore)
        {

        }

        public override async Task<CustomerDTO> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await this.AggregateStore.GetAsync(request.CustomerId);
            var result = CustomerDTO.Map(customer);
            return result;
        }
    }
}
