using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.RemoveInvoiceItem
{
    public class RemoveInvoiceItemHandler(IAggregateStore aggregateStore) : ICommandHandler<RemoveInvoiceItemCommand>
    {
        public async Task<Result> Handle(RemoveInvoiceItemCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.InvoiceId);
            var result = invoice.RemoveItem(request.InvoiceItemId);
            if (result.IsFailure)
            {
                return result.Error;
            }

            await aggregateStore.UpdateAsync(invoice);
            return Result.Success();
        }
    }
}
