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
        public Invoice(InvoiceId id, CustomerId customerId, int fiscalYear, InvoiceNumber number, Address customerAddress,
            DatePeriod performancePeriod, bool isReleased, DateTime? releasedOn, DateOnly? invoiceDate, Currency currency, 
            decimal tax, string taxDetails, OrderId? orderId, OfferId? offerId, List<InvoiceItem> items)
        {
            Id = id;
            CustomerId = customerId;
            FiscalYear = fiscalYear;
            Number = number;
            CustomerAddress = customerAddress;
            PerformancePeriod = performancePeriod;
            IsReleased = isReleased;
            ReleasedOn = releasedOn;
            InvoiceDate = invoiceDate;
            Currency = currency;
            Tax = tax;
            TaxDetails = taxDetails;
            Items = items;
            OrderId = orderId;
            OfferId = offerId;
        }

        public override InvoiceId Id { get; }
        public CustomerId CustomerId { get; }
        public int FiscalYear { get; }
        public InvoiceNumber Number { get; private set; }
        public Address CustomerAddress { get; set; }
        public DatePeriod PerformancePeriod { get; private set; }
        public decimal Tax { get; private set; }
        public string TaxDetails { get; private set; }
        public DateOnly? InvoiceDate {  get; private set; }
        public bool IsReleased { get; private set; }
        public DateTime? ReleasedOn { get; private set; }
        public OrderId? OrderId { get; private set; }
        public OfferId? OfferId { get; private set; }
        public List<InvoiceItem> Items { get; }
        public decimal Amount => Items.Sum(x => x.TotalPrice);
        public Currency Currency { get; }
        public DateOnly SalesReportDate => this.DetermineSalesReportDate();

        public static Invoice Create(InvoiceId id, CustomerId customerId, int fiscalYear, Address customerAddress, DatePeriod performancePeriod, Currency currency, decimal tax, string taxDetails,
            OrderId? orderId = null, OfferId? offerId = null)
        {
            return new Invoice(id, customerId, fiscalYear, InvoiceNumber.CreateTmp(), customerAddress, performancePeriod, false, null, null, currency, tax, taxDetails, orderId, offerId, new List<InvoiceItem>());
        }

        public void Edit(DatePeriod performancePeriod, decimal tax, string taxDetails, DateOnly? invoiceDate)
        {
            CheckCanEdit();
            this.PerformancePeriod = performancePeriod;
            this.Tax = tax;
            this.TaxDetails = taxDetails;
            this.InvoiceDate = invoiceDate;
        }

        public InvoiceItem AddItem(InvoiceItemId id, string details, Quantity quantity, decimal unitPrice, ProductId? productId = null)
        {
            CheckCanEdit();
            var position = Items.OrderByDescending(x => x.Position).FirstOrDefault()?.Position + 1 ?? 1;
            var result = new InvoiceItem(id, Id, position, details, quantity, unitPrice, productId);
            Items.Add(result);
            return result;
        }

        public InvoiceItem EditItem(InvoiceItemId id, string details, Quantity quantity, decimal unitPrice)
        {
            CheckCanEdit();
            var item = this.Items.Find(x => x.Id == id)!;
            item.Edit(details, quantity, unitPrice);
            return item;
        }

        public void RemoveItem(InvoiceItemId id)
        {
            CheckCanEdit();
            var item = this.Items.Find(x => x.Id == id)!;
            this.Items.Remove(item);
            this.ReorderItems();
        }

        public void Release(InvoiceNumber numberIfEmpty, DateTime? publishedOn)
        {
            CheckCanEdit();
            if (this.Number.IsTemporary && (numberIfEmpty == null || numberIfEmpty.IsTemporary))
            {
                throw new DomainException("Rechungsnummer ist ungültig");
            }
            if (this.Items.Count == 0)
            {
                throw new DomainException("Keine Positionen vorhanden");
            }
            this.Number = Number.IsTemporary ? numberIfEmpty : Number;
            this.ReleasedOn = publishedOn.HasValue ? publishedOn : DateTime.Now;
            this.IsReleased = true;
            this.InvoiceDate = this.InvoiceDate.HasValue ? this.InvoiceDate : DateOnly.FromDateTime(this.ReleasedOn.Value);
        }

        public void WithdrawRelease()
        {
            this.IsReleased = false;
        }

        private void ReorderItems()
        {
            var pos = 1;
            foreach (var item in this.Items.OrderBy(x => x.Position)) 
            { 
                item.EditPosition(pos++);
            }
        }

        private DateOnly DetermineSalesReportDate()
        {
            if (this.ReleasedOn != null)
            {
                return DateOnly.FromDateTime(this.ReleasedOn.Value);
            }
            else if (this.PerformancePeriod?.To.HasValue ?? false)
            {
                return this.PerformancePeriod.To.Value;
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
            if (this.IsReleased)
            {
                throw new DomainException("Rechnung wurde bereits veröffentlicht.");
            }
        }
    }
}

