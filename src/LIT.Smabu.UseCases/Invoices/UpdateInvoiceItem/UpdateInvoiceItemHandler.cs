using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.Shared.Interfaces;
using LIT.Smabu.UseCases.Invoices;

namespace LIT.Smabu.UseCases.Invoices.UpdateInvoiceItem
{
    public class UpdateInvoiceItemHandler : ICommandHandler<UpdateInvoiceItemCommand, InvoiceItemDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public UpdateInvoiceItemHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<InvoiceItemDTO> Handle(UpdateInvoiceItemCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.InvoiceId);
            var invoiceItem = invoice.UpdateItem(request.Id, request.Details, request.Quantity, request.UnitPrice);
            await aggregateStore.UpdateAsync(invoice);
            return InvoiceItemDTO.From(invoiceItem);
        }
    }
}
