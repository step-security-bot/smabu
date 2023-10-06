using LIT.Smabu.Shared.Invoices;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Queries
{
    public record GetInvoicesQuery : IRequest<InvoiceDTO[]>
    {
    }
}
