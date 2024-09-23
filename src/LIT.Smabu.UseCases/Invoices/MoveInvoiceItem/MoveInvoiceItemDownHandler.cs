using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.MoveInvoiceItem
{
    public class MoveInvoiceItemDownHandler(IAggregateStore aggregateStore) : ICommandHandler<MoveInvoiceItemDownCommand, bool>
    {
        public async Task<Result<bool>> Handle(MoveInvoiceItemDownCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.InvoiceId);
            invoice.MoveItemDown(request.Id);
            await aggregateStore.UpdateAsync(invoice);
            return true;
        }
    }
}
