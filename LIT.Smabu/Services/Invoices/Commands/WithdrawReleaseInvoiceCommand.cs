using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Shared.Invoices;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Commands
{
    public record WithdrawReleaseInvoiceCommand : IRequest<InvoiceDTO>
    {
        public required InvoiceId Id { get; set; }
    }
}
