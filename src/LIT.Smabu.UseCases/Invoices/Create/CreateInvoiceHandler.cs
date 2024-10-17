using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Invoices.Create
{
    public class CreateInvoiceHandler(IAggregateStore aggregateStore) : ICommandHandler<CreateInvoiceCommand, InvoiceId>
    {
        public async Task<Result<InvoiceId>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var customer = await aggregateStore.GetByAsync(request.CustomerId);
            var invoice = Invoice.Create(request.Id, request.CustomerId, request.FiscalYear, customer.MainAddress, request.PerformancePeriod!,
                request.Currency, request.TaxRate ?? TaxRate.Default, request.OrderId, request.OfferId);
            await aggregateStore.CreateAsync(invoice);
            return invoice.Id;
        }
    }
}
