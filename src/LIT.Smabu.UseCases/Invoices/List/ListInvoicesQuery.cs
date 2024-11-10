using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Invoices.List
{
    public record ListInvoicesQuery : IQuery<InvoiceDTO[]>
    {
        public CustomerId? CustomerId { get; init; }
    }
}
