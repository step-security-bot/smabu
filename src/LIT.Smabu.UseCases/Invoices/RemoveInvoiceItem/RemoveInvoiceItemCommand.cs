using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Invoices.RemoveInvoiceItem
{
    public record RemoveInvoiceItemCommand(InvoiceItemId InvoiceItemId, InvoiceId InvoiceId) : ICommand
    {

    }
}
