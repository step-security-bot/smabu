using LIT.Smabu.Shared.Interfaces;
using LIT.Smabu.UseCases.Invoices;
using MediatR;

namespace LIT.Smabu.UseCases.Invoices.Get
{
    public class GetInvoiceHandler : IRequestHandler<GetInvoiceQuery, InvoiceDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public GetInvoiceHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<InvoiceDTO> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.Id);
            var customer = await aggregateStore.GetByAsync(invoice.CustomerId);
            var result = InvoiceDTO.From(invoice, customer);
            return result;
        }
    }
}
