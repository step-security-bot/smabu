using LIT.Smabu.Domain.Exceptions;
using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.Contracts;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.OrderAggregate;
using LIT.Smabu.Domain.ProductAggregate;

namespace LIT.Smabu.Domain.InvoiceAggregate
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
        public DateOnly? InvoiceDate { get; private set; }
        public bool IsReleased { get; private set; }
        public DateTime? ReleasedOn { get; private set; }
        public OrderId? OrderId { get; private set; }
        public OfferId? OfferId { get; private set; }
        public List<InvoiceItem> Items { get; }
        public decimal Amount => Items.Sum(x => x.TotalPrice);
        public Currency Currency { get; }
        public DateOnly SalesReportDate => DetermineSalesReportDate();

        public static Invoice Create(InvoiceId id, CustomerId customerId, int fiscalYear, Address customerAddress, DatePeriod performancePeriod, Currency currency, decimal tax, string taxDetails,
            OrderId? orderId = null, OfferId? offerId = null)
        {
            return new Invoice(id, customerId, fiscalYear, InvoiceNumber.CreateTmp(), customerAddress, performancePeriod, false, null, null, currency, tax, taxDetails, orderId, offerId, []);
        }

        public void Update(DatePeriod performancePeriod, decimal tax, string taxDetails, DateOnly? invoiceDate)
        {
            CheckCanEdit();
            PerformancePeriod = performancePeriod;
            Tax = tax;
            TaxDetails = taxDetails;
            InvoiceDate = invoiceDate;
        }

        public void Delete()
        {
            this.CheckCanEdit();
        }

        public InvoiceItem AddItem(InvoiceItemId id, string details, Quantity quantity, decimal unitPrice, ProductId? productId = null)
        {
            CheckCanEdit();
            if (string.IsNullOrWhiteSpace(details))
            {
                throw new DomainException("Details dürfen nicht leer sein.");
            }
            var position = Items.OrderByDescending(x => x.Position).FirstOrDefault()?.Position + 1 ?? 1;
            var result = new InvoiceItem(id, Id, position, details, quantity, unitPrice, productId);
            Items.Add(result);
            return result;
        }

        public InvoiceItem UpdateItem(InvoiceItemId id, string details, Quantity quantity, decimal unitPrice)
        {
            CheckCanEdit();
            var item = Items.Find(x => x.Id == id)!;
            item.Edit(details, quantity, unitPrice);
            return item;
        }

        public void RemoveItem(InvoiceItemId id)
        {
            CheckCanEdit();
            var item = Items.Find(x => x.Id == id)!;
            Items.Remove(item);
            ReorderItems();
        }

        public void MoveItemDown(InvoiceItemId id)
        {
            CheckCanEdit();
            var itemToMove = this.Items.Find(x => x.Id == id)!;
            var itemToMoveCurrentIdx = this.Items.IndexOf(itemToMove);
            if(itemToMoveCurrentIdx == Items.Count - 1)
            {
                throw new DomainException("Bereits am Ende der Liste");
            }
            this.Items.Remove(itemToMove);
            this.Items.Insert(itemToMoveCurrentIdx + 1, itemToMove);
            this.ReorderItems();
        }

        public void MoveItemUp(InvoiceItemId id)
        {
            CheckCanEdit();
            var itemToMove = this.Items.Find(x => x.Id == id)!;
            var itemToMoveCurrentIdx = this.Items.IndexOf(itemToMove);
            if (itemToMoveCurrentIdx == 0)
            {
                throw new DomainException("Bereits am Anfang der Liste");
            }
            this.Items.Remove(itemToMove);
            this.Items.Insert(itemToMoveCurrentIdx - 1, itemToMove);
            this.ReorderItems();
        }

        public void Release(InvoiceNumber numberIfEmpty, DateTime? releasedOn)
        {
            CheckCanEdit();
            if (Number.IsTemporary && (numberIfEmpty == null || numberIfEmpty.IsTemporary))
            {
                throw new DomainException("Rechungsnummer ist ungültig");
            }
            if (Items.Count == 0)
            {
                throw new DomainException("Keine Positionen vorhanden");
            }
            Number = Number.IsTemporary ? numberIfEmpty : Number;
            ReleasedOn = releasedOn.HasValue ? releasedOn : DateTime.Now;
            IsReleased = true;
            InvoiceDate = InvoiceDate.HasValue ? InvoiceDate : DateOnly.FromDateTime(ReleasedOn.Value);
        }

        public void WithdrawRelease()
        {
            IsReleased = false;
        }

        private void ReorderItems()
        {
            var pos = 1;
            foreach (var item in Items)
            {
                item.EditPosition(pos++);
            }
        }

        private DateOnly DetermineSalesReportDate()
        {
            if (ReleasedOn != null)
            {
                return DateOnly.FromDateTime(ReleasedOn.Value);
            }
            else if (PerformancePeriod?.To.HasValue ?? false)
            {
                return PerformancePeriod.To.Value;
            }
            else if (PerformancePeriod?.From != null)
            {
                return PerformancePeriod.From;
            }
            else if (Meta != null)
            {
                return DateOnly.FromDateTime(Meta!.CreatedOn);
            }
            else
            {
                return DateOnly.FromDateTime(DateTime.Now);
            }
        }

        private void CheckCanEdit()
        {
            if (IsReleased)
            {
                throw new DomainException("Rechnung wurde bereits freigegeben.");
            }
        }
    }
}

