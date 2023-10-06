using LIT.Smabu.Domain.Shared.Customers;
using MediatR;

namespace LIT.Smabu.Domain.Shared.Customers.Queries
{
    public class GetCustomerDetailsQuery : IRequest<GetCustomerDetailsResponse>
    {
        public GetCustomerDetailsQuery(CustomerId customerId)
        {
            CustomerId = customerId;
        }

        public CustomerId CustomerId { get; }
    }
}
