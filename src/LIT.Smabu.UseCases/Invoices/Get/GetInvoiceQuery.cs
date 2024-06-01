using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.Get
{
    public record GetInvoiceQuery : IQuery<InvoiceDTO>
    {
        public GetInvoiceQuery(InvoiceId id)
        {
            Id = id;
        }

        public InvoiceId Id { get; }
    }
}