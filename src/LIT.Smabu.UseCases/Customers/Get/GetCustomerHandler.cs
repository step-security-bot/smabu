using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Customers.Get
{
    public class GetCustomerHandler : IQueryHandler<GetCustomerQuery, CustomerDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public GetCustomerHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<CustomerDTO> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await aggregateStore.GetByAsync(request.CustomerId);
            var result = CustomerDTO.CreateFrom(customer);
            return result;
        }
    }
}
