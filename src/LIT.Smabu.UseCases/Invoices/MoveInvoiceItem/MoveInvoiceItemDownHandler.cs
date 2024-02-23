using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Invoices.MoveInvoiceItem
{
    public class MoveInvoiceItemDownHandler : ICommandHandler<MoveInvoiceItemDownCommand, bool>
    {
        private readonly IAggregateStore aggregateStore;

        public MoveInvoiceItemDownHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<bool> Handle(MoveInvoiceItemDownCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.InvoiceId);
            invoice.MoveItemDown(request.Id);
            await aggregateStore.UpdateAsync(invoice);
            return true;
        }
    }
}
