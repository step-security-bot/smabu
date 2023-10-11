using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Contracts;
using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Domain.Shared.Exceptions;
using LIT.Smabu.Domain.Shared.Offers;
using LIT.Smabu.Domain.Shared.Orders;
using LIT.Smabu.Domain.Shared.Products;

namespace LIT.Smabu.Domain.Shared.Invoices
{
    public class Invoice : AggregateRoot<InvoiceId>
    {
        public Invoice(InvoiceId id, CustomerId customerId, int fiscalYear, InvoiceNumber number, DatePeriod performancePeriod,
            bool isPublished, DateTime? publishedOn, Currency currency, decimal tax, string taxDetails,
            OrderId? orderId, OfferId? offerId, List<InvoiceLine> invoiceLines)
        {
            Id = id;
            CustomerId = customerId;
            FiscalYear = fiscalYear;
            Number = number;
            PerformancePeriod = performancePeriod;
            IsPublished = isPublished;
            PublishedOn = publishedOn;
            Currency = currency;
            Tax = tax;
            TaxDetails = taxDetails;
            InvoiceLines = invoiceLines;
            OrderId = orderId;
            OfferId = offerId;
        }

        public override InvoiceId Id { get; }
        public CustomerId CustomerId { get; }
        public int FiscalYear { get; }
        public InvoiceNumber Number { get; private set; }
        public DatePeriod PerformancePeriod { get; private set; }
        public DateOnly? InvoiceDate {  get; private set; }
        public bool IsPublished { get; private set; }
        public DateTime? PublishedOn { get; private set; }
        public decimal Tax { get; private set; }
        public string TaxDetails { get; private set; }
        public OrderId? OrderId { get; private set; }
        public OfferId? OfferId { get; private set; }
        public List<InvoiceLine> InvoiceLines { get; }
        public decimal Amount => InvoiceLines.Sum(x => x.TotalPrice);
        public Currency Currency { get; }
        public DateOnly SalesReportDate => this.DetermineSalesReportDate();

        public static Invoice Create(InvoiceId id, CustomerId customerId, int fiscalYear, DatePeriod performancePeriod, Currency currency, decimal tax, string taxDetails,
            OrderId? orderId = null, OfferId? offerId = null)
        {
            return new Invoice(id, customerId, fiscalYear, InvoiceNumber.CreateTmp(), performancePeriod, false, null, currency, tax, taxDetails, orderId, offerId, new List<InvoiceLine>());
        }

        public void Edit(DatePeriod performancePeriod, decimal tax, string taxDetails, DateOnly? invoiceDate)
        {
            CheckCanEdit();
            this.PerformancePeriod = performancePeriod;
            this.Tax = tax;
            this.TaxDetails = taxDetails;
            this.InvoiceDate = invoiceDate;
        }

        public InvoiceLine AddInvoiceLine(string details, Quantity quantity, decimal unitPrice, ProductId? productId = null)
        {
            CheckCanEdit();
            var id = new InvoiceLineId(Guid.NewGuid());
            var position = InvoiceLines.OrderByDescending(x => x.Position).FirstOrDefault()?.Position + 1 ?? 1;
            var result = new InvoiceLine(id, Id, position, details, quantity, unitPrice, productId);
            InvoiceLines.Add(result);
            return result;
        }

        public InvoiceLine EditInvoiceLine(InvoiceLineId id, string details, Quantity quantity, decimal unitPrice)
        {
            CheckCanEdit();
            var invoiceLine = this.InvoiceLines.Find(x => x.Id == id)!;
            invoiceLine.Edit(details, quantity, unitPrice);
            return invoiceLine;
        }

        public void Publish(InvoiceNumber numberIfEmpty, DateOnly? invoiceDate)
        {
            CheckCanEdit();
            if (this.Number.IsTemporary && (numberIfEmpty == null || numberIfEmpty.IsTemporary))
            {
                throw new DomainException("Rechungsnummer ist ungültig");
            }
            this.InvoiceDate = invoiceDate.HasValue ? invoiceDate : DateOnly.FromDateTime(DateTime.Now);
            this.Number = Number.IsTemporary ? numberIfEmpty : Number;
            this.PublishedOn = DateTime.Now;
            this.IsPublished = true;
        }

        public void WithdrawPublication()
        {
            this.IsPublished = false;
        }

        private DateOnly DetermineSalesReportDate()
        {
            if (this.PublishedOn != null)
            {
                return DateOnly.FromDateTime(this.PublishedOn.Value);
            }
            else if (this.PerformancePeriod?.To != null)
            {
                return this.PerformancePeriod.To;
            }
            else if (this.PerformancePeriod?.From != null)
            {
                return this.PerformancePeriod.From;
            }
            else if (this.Meta != null)
            {
                return DateOnly.FromDateTime(this.Meta!.CreatedOn);
            }
            else
            {
                return DateOnly.FromDateTime(DateTime.Now);
            }
        }

        private void CheckCanEdit()
        {
            if (this.IsPublished)
            {
                throw new DomainException("Rechnung wurde bereits veröffentlicht.");
            }
        }
    }
}

