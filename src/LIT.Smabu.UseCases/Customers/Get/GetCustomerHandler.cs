using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Customers.Get
{
    public class GetCustomerHandler(IAggregateStore aggregateStore) : IQueryHandler<GetCustomerQuery, CustomerDTO>
    {
        public async Task<Result<CustomerDTO>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await aggregateStore.GetByAsync(request.CustomerId);
            var result = CustomerDTO.Create(customer);
            return result;
        }
    }
}
