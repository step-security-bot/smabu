using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Invoices.Update
{
    public class UpdateInvoiceHandler : ICommandHandler<UpdateInvoiceCommand, InvoiceDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public UpdateInvoiceHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<InvoiceDTO> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await aggregateStore.GetByAsync(request.Id);
            var customer = await aggregateStore.GetByAsync(invoice.CustomerId);
            invoice.Update(request.PerformancePeriod, request.Tax, request.TaxDetails, request.InvoiceDate);
            await aggregateStore.UpdateAsync(invoice);
            return InvoiceDTO.From(invoice, customer);
        }
    }
}
