using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.GetWithItems
{
    public record GetInvoiceWithItemsQuery(InvoiceId Id) : IQuery<InvoiceWithItemsDTO>
    {

    }
}