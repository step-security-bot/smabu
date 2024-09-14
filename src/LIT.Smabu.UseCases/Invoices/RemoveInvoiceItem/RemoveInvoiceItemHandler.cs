using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.RemoveInvoiceItem
{
    public class RemoveInvoiceItemHandler(IAggregateStore aggregateStore) : ICommandHandler<RemoveInvoiceItemCommand, bool>
    {
        public async Task<bool> Handle(RemoveInvoiceItemCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.InvoiceId);
            invoice.RemoveItem(request.InvoiceItemId);
            await aggregateStore.UpdateAsync(invoice);
            return true;
        }
    }
}
