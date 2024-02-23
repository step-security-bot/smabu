using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Invoices.RemoveInvoiceItem
{
    public record RemoveInvoiceItemCommand : ICommand<bool>
    {
        public required InvoiceItemId Id { get; set; }
        public required InvoiceId InvoiceId { get; set; }
    }
}
