using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Shared.BusinessDomain;
using LIT.Smabu.Shared.BusinessDomain.Customer;
using LIT.Smabu.Shared.BusinessDomain.Invoice;
using LIT.Smabu.Shared.BusinessDomain.Offer;
using LIT.Smabu.Shared.BusinessDomain.Order;

namespace LIT.Smabu.Server.Services.Business
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
            var invoice = Invoice.Create(new InvoiceId(Guid.NewGuid()), customerId, performancePeriod, tax, taxDetails, orderId, offerId);
            await this.aggregateStore.AddOrUpdateAsync(invoice);
            return invoice;
        }
    }
}
