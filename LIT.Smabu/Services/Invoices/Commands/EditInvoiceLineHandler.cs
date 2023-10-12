using LIT.Smabu.Business.Service.Invoices.Mappings;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Invoices;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Commands
{
    public class EditInvoiceLineHandler : IRequestHandler<EditInvoiceLineCommand, InvoiceItemDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public EditInvoiceLineHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<InvoiceItemDTO> Handle(EditInvoiceLineCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.InvoiceId);
            var invoiceLine = invoice.EditItem(request.Id, request.Details, request.Quantity, request.UnitPrice);
            await aggregateStore.AddOrUpdateAsync(invoice);
            return new InvoiceMapper(this.aggregateStore).Map(invoiceLine);
        }
    }
}
