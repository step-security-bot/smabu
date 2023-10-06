using LIT.Smabu.Domain.Shared.Common;
using MediatR;

namespace LIT.Smabu.Domain.Shared.Invoices.Commands
{
    public record AddInvoiceLineCommand : IRequest<InvoiceLineId>
    {
        public required InvoiceId InvoiceId { get; set; }
        public required string Details { get; set; }
        public required Quantity Quantity { get; set; }
        public required decimal UnitPrice { get; set; }
    }
}
