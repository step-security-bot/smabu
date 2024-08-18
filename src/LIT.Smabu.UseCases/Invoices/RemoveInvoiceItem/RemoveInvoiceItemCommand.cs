using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.RemoveInvoiceItem
{
    public record RemoveInvoiceItemCommand(InvoiceId InvoiceId, InvoiceItemId InvoiceItemId) : ICommand<bool>
    {

    }
}
