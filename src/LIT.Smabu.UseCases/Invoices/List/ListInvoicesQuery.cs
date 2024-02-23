using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Invoices.List
{
    public record ListInvoicesQuery : IQuery<InvoiceDTO[]>
    {
    }
}
