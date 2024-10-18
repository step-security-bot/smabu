using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Invoices.List
{
    public record ListInvoicesQuery : IQuery<InvoiceDTO[]>
    {
    }
}
