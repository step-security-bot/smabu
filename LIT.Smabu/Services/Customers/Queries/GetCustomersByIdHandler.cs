using LIT.Smabu.Business.Service.Mapping;
using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Customers;
using MediatR;

namespace LIT.Smabu.Business.Service.Customers.Queries
{
    public class GetCustomersByIdHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDTO>
    {
        private readonly IAggregateStore aggregateStore;
        private readonly IMapper mapper;

        public GetCustomersByIdHandler(IAggregateStore aggregateStore, IMapper mapper)
        {
            this.aggregateStore = aggregateStore;
            this.mapper = mapper;
        }

        public async Task<CustomerDTO> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await this.aggregateStore.GetAsync(request.CustomerId);
            var result = mapper.Map<Customer, CustomerDTO>(customer);
            return result;
        }
    }
}
