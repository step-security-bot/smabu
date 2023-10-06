using MediatR;

namespace LIT.Smabu.Domain.Shared.Invoices.Queries
{
    public record GetAllInvoicesQuery : IRequest<GetAllInvoicesResponse[]>
    {
    }
}
