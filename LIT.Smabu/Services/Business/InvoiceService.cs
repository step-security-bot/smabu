using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Shared.Entities.Business;
using LIT.Smabu.Shared.Entities.Business.CustomerAggregate;
using LIT.Smabu.Shared.Entities.Business.InvoiceAggregate;
using LIT.Smabu.Shared.Entities.Business.OfferAggregate;
using LIT.Smabu.Shared.Entities.Business.OrderAggregate;
using LIT.Smabu.Shared.Entities.Business.ProductAggregate;

namespace LIT.Smabu.Service.Business
{
    public class InvoiceService
    {
        private readonly IAggregateStore aggregateStore;

        public InvoiceService(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<Invoice> CreateAsync(CustomerId customerId, Period performancePeriod, decimal tax, string taxDetails,
            OrderId? orderId = null, OfferId? offerId = null)
        {
            var invoice = Invoice.Create(new InvoiceId(Guid.NewGuid()), customerId, new InvoiceNumber(1), performancePeriod, tax, taxDetails, orderId, offerId);
            await aggregateStore.AddOrUpdateAsync(invoice);
            return invoice;
        }

        public async Task<Invoice> AddInvoiceLineAsync(InvoiceId invoiceId, string details, Quantity quantity, decimal unitPrice, Currency currency, ProductId? productId = null)
        {
            var invoice = aggregateStore.Get<Invoice, InvoiceId>(invoiceId);
            invoice.AddInvoiceLine(details, quantity, unitPrice, currency, productId);
            await aggregateStore.AddOrUpdateAsync(invoice);
            return invoice;
        }
    }
}
