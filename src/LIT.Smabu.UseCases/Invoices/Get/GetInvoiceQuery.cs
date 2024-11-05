using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Invoices.Get
{
    public record GetInvoiceQuery : IQuery<InvoiceDTO>
    {
        public GetInvoiceQuery(InvoiceId invoiceId)
        {
            InvoiceId = invoiceId;
        }

        public InvoiceId InvoiceId { get; }
        public bool WithItems { get; set; }
    }
}