using MediatR;

namespace LIT.Smabu.Shared.Domain.Customers.Queries
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
