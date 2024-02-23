using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Invoices.MoveInvoiceItem
{
    public record MoveInvoiceItemDownCommand(InvoiceItemId Id, InvoiceId InvoiceId) : ICommand<bool>
    {
    }
}
