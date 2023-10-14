using LIT.Smabu.Business.Service.Invoices.Mappers;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Invoices;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Commands
{
    public class EditInvoiceHandler : IRequestHandler<EditInvoiceCommand, InvoiceDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public EditInvoiceHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<InvoiceDTO> Handle(EditInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.Id);
            invoice.Edit(request.PerformancePeriod, request.Tax, request.TaxDetails, request.InvoiceDate);
            await aggregateStore.AddOrUpdateAsync(invoice);
            return await new InvoiceMapper(this.aggregateStore).MapAsync(invoice);
        }
    }
}
