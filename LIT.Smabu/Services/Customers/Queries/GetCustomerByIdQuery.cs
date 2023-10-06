using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Shared.Customers;
using MediatR;

namespace LIT.Smabu.Business.Service.Customers.Queries
{
    public class GetCustomerByIdQuery : IRequest<CustomerDTO>
    {
        public GetCustomerByIdQuery(CustomerId customerId)
        {
            CustomerId = customerId;
        }

        public CustomerId CustomerId { get; }
    }
}
