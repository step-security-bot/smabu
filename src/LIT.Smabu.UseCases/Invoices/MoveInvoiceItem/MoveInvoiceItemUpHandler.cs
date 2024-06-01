using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.MoveInvoiceItem
{
    public class MoveInvoiceItemUpHandler(IAggregateStore aggregateStore) : ICommandHandler<MoveInvoiceItemUpCommand, bool>
    {
        public async Task<bool> Handle(MoveInvoiceItemUpCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.InvoiceId);
            invoice.MoveItemUp(request.Id);
            await aggregateStore.UpdateAsync(invoice);
            return true;
        }
    }
}
