using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Invoices.AddInvoiceItem
{
    public class AddInvoiceItemHandler(IAggregateStore aggregateStore) : ICommandHandler<AddInvoiceItemCommand, InvoiceItemId>
    {
        public async Task<Result<InvoiceItemId>> Handle(AddInvoiceItemCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.InvoiceId);
            var invoiceLineResult = invoice.AddItem(request.Id, request.Details, request.Quantity, request.UnitPrice);
            if (invoiceLineResult.IsFailure)
            {
                return invoiceLineResult.Error;
            }

            await aggregateStore.UpdateAsync(invoice);
            return invoiceLineResult.Value!.Id;
        }
    }
}
