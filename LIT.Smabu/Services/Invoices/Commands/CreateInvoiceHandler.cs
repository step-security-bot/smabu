using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using MediatR;

namespace LIT.Smabu.Business.Service.Invoices.Commands
{
    public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, InvoiceId>
    {
        private readonly IAggregateStore aggregateStore;

        public CreateInvoiceHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<InvoiceId> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var customer = await this.aggregateStore.GetByAsync(request.CustomerId);
            var invoice = Invoice.Create(request.Id, request.CustomerId, request.FiscalYear, customer.MainAddress, request.PerformancePeriod,
                request.Currency, request.Tax, request.TaxDetails, request.OrderId, request.OfferId);
            await aggregateStore.AddOrUpdateAsync(invoice);
            return invoice.Id;
        }
    }
}
