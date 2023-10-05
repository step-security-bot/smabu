using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Service.ReadModels;
using LIT.Smabu.Shared.Domain.Business.Common;
using LIT.Smabu.Shared.Domain.Business.CustomerAggregate;
using LIT.Smabu.Shared.Domain.Business.InvoiceAggregate;
using LIT.Smabu.Shared.Domain.Business.OfferAggregate;
using LIT.Smabu.Shared.Domain.Business.OrderAggregate;
using LIT.Smabu.Shared.Domain.Business.ProductAggregate;

namespace LIT.Smabu.Service.Business
{
    public class InvoiceService
    {
        private readonly IAggregateStore aggregateStore;
        private readonly InvoiceReadModel invoiceReadModel;

        public InvoiceService(IAggregateStore aggregateStore, InvoiceReadModel invoiceReadModel)
        {
            this.aggregateStore = aggregateStore;
            this.invoiceReadModel = invoiceReadModel;
        }

        public async Task<Invoice> CreateAsync(CustomerId customerId, Period performancePeriod, decimal tax, string taxDetails,
            OrderId? orderId = null, OfferId? offerId = null, InvoiceNumber? invoiceNumber = null)
        {
            invoiceNumber ??= CreateNewNumber(performancePeriod.To.Year);
            var invoice = Invoice.Create(new InvoiceId(Guid.NewGuid()), customerId, invoiceNumber, performancePeriod,
                Currency.GetEuro(), tax, taxDetails, orderId, offerId);
            await aggregateStore.AddOrUpdateAsync(invoice);
            return invoice;
        }

       public async Task<Invoice> AddInvoiceLineAsync(InvoiceId invoiceId, string details, Quantity quantity, decimal unitPrice, ProductId? productId = null)
        {
            var invoice = aggregateStore.Get<Invoice, InvoiceId>(invoiceId);
            invoice.AddInvoiceLine(details, quantity, unitPrice, productId);
            await aggregateStore.AddOrUpdateAsync(invoice);
            return invoice;
        }

        private InvoiceNumber CreateNewNumber(int year)
        {
            var lastNumber = invoiceReadModel.GetOverview()
                .Select(x => x.Number)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            return lastNumber == null ? InvoiceNumber.CreateFirst(year) : InvoiceNumber.CreateNext(lastNumber);
        }
    }
}
