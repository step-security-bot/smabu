using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.UpdateInvoiceItem
{
    public class UpdateInvoiceItemHandler(IAggregateStore aggregateStore) : ICommandHandler<UpdateInvoiceItemCommand, InvoiceItemDTO>
    {
        public async Task<InvoiceItemDTO> Handle(UpdateInvoiceItemCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.InvoiceId);
            var invoiceItem = invoice.UpdateItem(request.Id, request.Details, request.Quantity, request.UnitPrice);
            await aggregateStore.UpdateAsync(invoice);
            return InvoiceItemDTO.CreateFrom(invoiceItem);
        }
    }
}
