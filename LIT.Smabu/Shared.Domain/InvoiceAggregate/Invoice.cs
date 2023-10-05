using LIT.Smabu.Shared.Domain.Common;
using LIT.Smabu.Shared.Domain.Contracts;
using LIT.Smabu.Shared.Domain.CustomerAggregate;
using LIT.Smabu.Shared.Domain.OfferAggregate;
using LIT.Smabu.Shared.Domain.OrderAggregate;
using LIT.Smabu.Shared.Domain.ProductAggregate;

namespace LIT.Smabu.Shared.Domain.InvoiceAggregate
{
    public class Invoice : AggregateRoot<InvoiceId>
    {
        public Invoice(InvoiceId id, CustomerId customerId, InvoiceNumber number, Period performancePeriod, Currency currency, decimal tax, string taxDetails,
            OrderId? orderId, OfferId? offerId, List<InvoiceLine> invoiceLines)
        {
            Id = id;
            CustomerId = customerId;
            Number = number;
            PerformancePeriod = performancePeriod;
            Currency = currency;
            OrderId = orderId;
            OfferId = offerId;
            Tax = tax;
            TaxDetails = taxDetails;
            InvoiceLines = invoiceLines;
        }

        public override InvoiceId Id { get; }
        public CustomerId CustomerId { get; }
        public InvoiceNumber Number { get; }
        public Period PerformancePeriod { get; private set; }
        public int FiscalYear => PerformancePeriod.To.Year;
        public decimal Tax { get; private set; }
        public string TaxDetails { get; private set; }
        public OrderId? OrderId { get; private set; }
        public OfferId? OfferId { get; private set; }
        public List<InvoiceLine> InvoiceLines { get; }
        public decimal Amount => InvoiceLines.Sum(x => x.TotalPrice);
        public Currency Currency { get; }

        public InvoiceLine AddInvoiceLine(string details, Quantity quantity, decimal unitPrice, ProductId? productId = null)
        {
            var id = new InvoiceLineId(Guid.NewGuid());
            var position = InvoiceLines.OrderByDescending(x => x.Position).FirstOrDefault()?.Position + 1 ?? 0;
            var result = new InvoiceLine(id, Id, position, details, quantity, unitPrice, productId);
            InvoiceLines.Add(result);
            return result;
        }

        public static Invoice Create(InvoiceId id, CustomerId customerId, InvoiceNumber number, Period performancePeriod, Currency currency, decimal tax, string taxDetails,
            OrderId? orderId = null, OfferId? offerId = null)
        {
            return new Invoice(id, customerId, number, performancePeriod, currency, tax, taxDetails, orderId, offerId, new List<InvoiceLine>());
        }
    }
}

