using MediatR;

namespace LIT.Smabu.Shared.Domain.Customers.Queries
{
    public record GetAllCustomersQuery : IRequest<GetAllCustomersResponse[]>
    {
    }
}
