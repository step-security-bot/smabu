using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Invoices.MoveInvoiceItem
{
    public class MoveInvoiceItemUpHandler : ICommandHandler<MoveInvoiceItemUpCommand, bool>
    {
        private readonly IAggregateStore aggregateStore;

        public MoveInvoiceItemUpHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<bool> Handle(MoveInvoiceItemUpCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.InvoiceId);
            invoice.MoveItemUp(request.Id);
            await aggregateStore.UpdateAsync(invoice);
            return true;
        }
    }
}
