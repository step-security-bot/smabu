using LIT.Smabu.Shared.Customers;
using MediatR;

namespace LIT.Smabu.Business.Service.Customers.Queries
{
    public record GetCustomersQuery : IRequest<CustomerDTO[]>
    {
    }
}
