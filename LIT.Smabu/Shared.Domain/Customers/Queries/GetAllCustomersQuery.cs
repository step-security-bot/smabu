using MediatR;

namespace LIT.Smabu.Domain.Shared.Customers.Queries
{
    public record GetAllCustomersQuery : IRequest<GetAllCustomersResponse[]>
    {
    }
}
