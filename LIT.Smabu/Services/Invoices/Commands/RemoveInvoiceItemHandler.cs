using LIT.Smabu.Infrastructure.Shared.Contracts;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Commands
{
    public class RemoveInvoiceItemHandler : IRequestHandler<RemoveInvoiceItemCommand>
    {
        private readonly IAggregateStore aggregateStore;

        public RemoveInvoiceItemHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task Handle(RemoveInvoiceItemCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.InvoiceId);
            invoice.RemoveItem(request.Id);
            await aggregateStore.AddOrUpdateAsync(invoice);
        }
    }
}
