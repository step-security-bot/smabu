using LIT.Smabu.Shared.BusinessDomain.Customer;
using LIT.Smabu.Shared.BusinessDomain.Offer;
using LIT.Smabu.Shared.BusinessDomain.Order;
using LIT.Smabu.Shared.BusinessDomain.Product;
using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.BusinessDomain.Invoice
{
    public class Invoice : IAggregateRoot<InvoiceId>, IInvoice
    {
        public Invoice(InvoiceId id, string displayName, Period performancePeriod, CustomerId customerId, decimal tax, string taxDetails,
            OrderId? orderId, OfferId? offerId, List<IInvoiceLine> invoiceLines)
        {
            Id = id;
            DisplayName = displayName;
            PerformancePeriod = performancePeriod;
            OrderId = orderId;
            OfferId = offerId;
            CustomerId = customerId;
            Tax = tax;
            TaxDetails = taxDetails;
            InvoiceLines = invoiceLines;
        }

        public InvoiceId Id { get; }
        public string DisplayName { get; }
        public Period PerformancePeriod { get; private set; }
        public int FiscalYear => PerformancePeriod.To.Year;
        public CustomerId CustomerId { get; private set; }
        public decimal Tax { get; private set; }
        public string TaxDetails { get; private set; }
        public OrderId? OrderId { get; private set; }
        public OfferId? OfferId { get; private set; }
        public List<IInvoiceLine> InvoiceLines { get; }

        public InvoiceLine AddInvoiceLine(string details, Quantity quantity, decimal unitPrice, Currency currency, ProductId? productId = null)
        {
            var id = new InvoiceLineId(Guid.NewGuid());
            var position = InvoiceLines.OrderByDescending(x => x.Position).FirstOrDefault()?.Position + 1 ?? 0;
            var displayName = $"{DisplayName}-Pos:{position}";
            var result = new InvoiceLine(id, Id, displayName, position, details, quantity, unitPrice, currency, productId);
            InvoiceLines.Add(result);
            return result;
        }

        public static Invoice Create(InvoiceId id, string displayName, Period performancePeriod, CustomerId customerId, decimal tax, string taxDetails,
            OrderId? orderId = null, OfferId? offerId = null)
        {
            return new Invoice(id, displayName, performancePeriod, customerId, tax, taxDetails, orderId, offerId, new List<IInvoiceLine>());
        }
    }
}

