using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.GetWithItems
{
    public class GetInvoiceWithItemsHandler(IAggregateStore aggregateStore) : IQueryHandler<GetInvoiceWithItemsQuery, InvoiceWithItemsDTO>
    {
        public async Task<InvoiceWithItemsDTO> Handle(GetInvoiceWithItemsQuery request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.Id);
            var customer = await aggregateStore.GetByAsync(invoice.CustomerId);
            var result = InvoiceWithItemsDTO.From(invoice, customer);
            return result;
        }
    }
}
