using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Shared.Invoices;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Commands
{
    public record EditInvoiceItemCommand : IRequest<InvoiceItemDTO>
    {
        public required InvoiceItemId Id { get; set; }
        public required InvoiceId InvoiceId { get; set; }
        public required string Details { get; set; }
        public required Quantity Quantity { get; set; }
        public required decimal UnitPrice { get; set; }
    }
}
