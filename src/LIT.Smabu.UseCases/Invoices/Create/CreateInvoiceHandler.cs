using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Invoices.Create
{
    public class CreateInvoiceHandler(IAggregateStore store) : ICommandHandler<CreateInvoiceCommand, InvoiceId>
    {
        public async Task<Result<InvoiceId>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var customer = await store.GetByAsync(request.CustomerId);
            var invoice = Invoice.Create(request.Id, request.CustomerId, request.FiscalYear, customer.MainAddress, request.PerformancePeriod!,
                request.Currency, request.TaxRate ?? TaxRate.Default, request.OrderId, request.OfferId);
            await store.CreateAsync(invoice);
            return invoice.Id;
        }
    }
}
