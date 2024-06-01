using LIT.Smabu.Domain.SeedWork;
using MediatR;

namespace LIT.Smabu.UseCases.Invoices.GetWithItems
{
    public class GetInvoiceWithItemsHandler : IRequestHandler<GetInvoiceWithItemsQuery, InvoiceWithItemsDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public GetInvoiceWithItemsHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<InvoiceWithItemsDTO> Handle(GetInvoiceWithItemsQuery request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.Id);
            var customer = await aggregateStore.GetByAsync(invoice.CustomerId);
            var result = InvoiceWithItemsDTO.From(invoice, customer);
            return result;
        }
    }
}
