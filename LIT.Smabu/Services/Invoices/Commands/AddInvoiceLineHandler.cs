using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Commands
{
    public class AddInvoiceLineHandler : IRequestHandler<AddInvoiceLineCommand, InvoiceItemId>
    {
        private readonly IAggregateStore aggregateStore;

        public AddInvoiceLineHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<InvoiceItemId> Handle(AddInvoiceLineCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.InvoiceId);
            var invoiceLine = invoice.AddItem(request.Details, request.Quantity, request.UnitPrice);
            await aggregateStore.AddOrUpdateAsync(invoice);
            return invoiceLine.Id;
        }
    }
}
