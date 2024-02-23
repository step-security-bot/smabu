using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Shared.Interfaces;
using LIT.Smabu.UseCases.Invoices;

namespace LIT.Smabu.UseCases.Invoices.Create
{
    public class CreateInvoiceHandler : ICommandHandler<CreateInvoiceCommand, InvoiceDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public CreateInvoiceHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<InvoiceDTO> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var customer = await aggregateStore.GetByAsync(request.CustomerId);
            var invoice = Invoice.Create(request.Id, request.CustomerId, request.FiscalYear, customer.MainAddress, request.PerformancePeriod!,
                request.Currency, request.Tax, request.TaxDetails, request.OrderId, request.OfferId);
            await aggregateStore.CreateAsync(invoice);
            return InvoiceDTO.From(invoice, customer);
        }
    }
}
