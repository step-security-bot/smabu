using LIT.Smabu.Domain.Shared.Invoices;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Commands
{
    public record RemoveInvoiceItemCommand : IRequest
    {
        public required InvoiceItemId Id { get; set; }
        public required InvoiceId InvoiceId { get; set; }
    }
}
