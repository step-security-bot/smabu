using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Infrastructure.DDD;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Commands
{
    public class AddInvoiceLineHandler : IRequestHandler<AddInvoiceLineCommand, InvoiceLineId>
    {
        private readonly IAggregateStore aggregateStore;

        public AddInvoiceLineHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<InvoiceLineId> Handle(AddInvoiceLineCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetAsync(request.InvoiceId);
            var invoiceLine = invoice.AddInvoiceLine(request.Details, request.Quantity, request.UnitPrice);
            await aggregateStore.AddOrUpdateAsync(invoice);
            return invoiceLine.Id;
        }
    }
}
