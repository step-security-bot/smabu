using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.Update
{
    public class UpdateInvoiceHandler(IAggregateStore aggregateStore) : ICommandHandler<UpdateInvoiceCommand, InvoiceDTO>
    {
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
