using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.AddInvoiceItem
{
    public class AddInvoiceItemHandler(IAggregateStore aggregateStore) : ICommandHandler<AddInvoiceItemCommand, InvoiceItemDTO>
    {
        public async Task<InvoiceItemDTO> Handle(AddInvoiceItemCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.InvoiceId);
            var invoiceLine = invoice.AddItem(request.Id, request.Details, request.Quantity, request.UnitPrice);
            await aggregateStore.UpdateAsync(invoice);
            return InvoiceItemDTO.From(invoiceLine);
        }
    }
}
