using LIT.Smabu.Business.Service.Customers.Mappers;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Customers;
using MediatR;

namespace LIT.Smabu.Business.Service.Customers.Queries
{
    public class GetCustomersByIdHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public GetCustomersByIdHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<CustomerDTO> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await this.aggregateStore.GetByAsync(request.CustomerId);
            var result = await new CustomerMapper(this.aggregateStore).MapAsync(customer);
            return result;
        }
    }
}
