using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.Delete
{
    public record DeleteInvoiceCommand(InvoiceId Id) : ICommand<bool>
    {

    }
}
