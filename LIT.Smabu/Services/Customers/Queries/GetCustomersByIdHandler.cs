﻿using LIT.Smabu.Infrastructure.DDD;
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
            var customer = await this.aggregateStore.GetAsync(request.CustomerId);
            var result = CustomerDTO.Map(customer);
            return result;
        }
    }
}
