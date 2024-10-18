using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Invoices.MoveInvoiceItem
{
    public class MoveInvoiceItemUpHandler(IAggregateStore aggregateStore) : ICommandHandler<MoveInvoiceItemUpCommand>
    {
        public async Task<Result> Handle(MoveInvoiceItemUpCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.InvoiceId);
            var result = invoice.MoveItemUp(request.Id);
            if (result.IsFailure)
            {
                return result.Error;
            }

            await aggregateStore.UpdateAsync(invoice);
            return Result.Success();
        }
    }
}
