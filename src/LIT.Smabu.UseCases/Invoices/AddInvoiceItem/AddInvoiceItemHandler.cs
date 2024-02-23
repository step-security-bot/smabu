using LIT.Smabu.Shared.Interfaces;
using MediatR;

namespace LIT.Smabu.UseCases.Invoices.AddInvoiceItem
{
    public class AddInvoiceItemHandler : ICommandHandler<AddInvoiceItemCommand, InvoiceItemDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public AddInvoiceItemHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<InvoiceItemDTO> Handle(AddInvoiceItemCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.InvoiceId);
            var invoiceLine = invoice.AddItem(request.Id, request.Details, request.Quantity, request.UnitPrice);
            await aggregateStore.UpdateAsync(invoice);
            return InvoiceItemDTO.From(invoiceLine);
        }
    }
}
