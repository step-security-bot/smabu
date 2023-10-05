using MediatR;

namespace LIT.Smabu.Shared.Domain.Customers.Queries
{
    public record GetAllInvoicesQuery : IRequest<GetAllInvoicesResponse[]>
    {
    }
}
