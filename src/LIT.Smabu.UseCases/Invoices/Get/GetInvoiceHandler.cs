using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.Get
{
    public class GetInvoiceHandler(IAggregateStore aggregateStore) : IQueryHandler<GetInvoiceQuery, InvoiceDTO>
    {
        public async Task<Result<InvoiceDTO>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.Id);
            var customer = await aggregateStore.GetByAsync(invoice.CustomerId);
            var result = InvoiceDTO.From(invoice, customer, request.WithItems);
            return result;
        }
    }
}
