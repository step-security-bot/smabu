using LIT.Smabu.Shared.Entities.Business.CustomerAggregate;
using LIT.Smabu.Shared.Entities.Business.OfferAggregate;
using LIT.Smabu.Shared.Entities.Business.OrderAggregate;
using LIT.Smabu.Shared.Entities.Business.ProductAggregate;

namespace LIT.Smabu.Shared.Entities.Business.InvoiceAggregate
{
    public class Invoice : AggregateRoot<InvoiceId>
    {
        public Invoice(InvoiceId id, CustomerId customerId, InvoiceNumber number, Period performancePeriod, decimal tax, string taxDetails,
            OrderId? orderId, OfferId? offerId, List<InvoiceLine> invoiceLines)
        {
            Id = id;
            CustomerId = customerId;
            Number = number;
            PerformancePeriod = performancePeriod;
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


        public InvoiceLine AddInvoiceLine(string details, Quantity quantity, decimal unitPrice, Currency currency, ProductId? productId = null)
        {
            var id = new InvoiceLineId(Guid.NewGuid());
            var position = InvoiceLines.OrderByDescending(x => x.Position).FirstOrDefault()?.Position + 1 ?? 0;
            var result = new InvoiceLine(id, Id, position, details, quantity, unitPrice, currency, productId);
            InvoiceLines.Add(result);
            return result;
        }

        public static Invoice Create(InvoiceId id, CustomerId customerId, InvoiceNumber number, Period performancePeriod, decimal tax, string taxDetails,
            OrderId? orderId = null, OfferId? offerId = null)
        {
            return new Invoice(id, customerId, number, performancePeriod, tax, taxDetails, orderId, offerId, new List<InvoiceLine>());
        }
    }
}

