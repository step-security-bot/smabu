using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Invoices.RemoveInvoiceItem
{
    public class RemoveInvoiceItemHandler : ICommandHandler<RemoveInvoiceItemCommand, bool>
    {
        private readonly IAggregateStore aggregateStore;

        public RemoveInvoiceItemHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<bool> Handle(RemoveInvoiceItemCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.InvoiceId);
            invoice.RemoveItem(request.Id);
            await aggregateStore.UpdateAsync(invoice);
            return true;
        }
    }
}
