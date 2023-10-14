using LIT.Smabu.Business.Service.Invoices.Mappers;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Invoices;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Commands
{
    public class EditInvoiceItemHandler : IRequestHandler<EditInvoiceItemCommand, InvoiceItemDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public EditInvoiceItemHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<InvoiceItemDTO> Handle(EditInvoiceItemCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.InvoiceId);
            var invoiceItem = invoice.EditItem(request.Id, request.Details, request.Quantity, request.UnitPrice);
            await aggregateStore.AddOrUpdateAsync(invoice);
            return new InvoiceMapper(this.aggregateStore).Map(invoiceItem);
        }
    }
}
