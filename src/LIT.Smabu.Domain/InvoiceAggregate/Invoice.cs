using LIT.Smabu.Domain.OrderAggregate;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.ProductAggregate;
using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.Domain.Exceptions;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public class Invoice(InvoiceId id, CustomerId customerId, int fiscalYear, InvoiceNumber number,
                   Address customerAddress, DatePeriod performancePeriod, bool isReleased, DateTime? releasedOn,
                   DateOnly? invoiceDate, Currency currency, decimal tax, string taxDetails, OrderId? orderId,
                   OfferId? offerId, List<InvoiceItem> items) : AggregateRoot<InvoiceId>
    {
        public override InvoiceId Id { get; } = id;
        public CustomerId CustomerId { get; } = customerId;
        public int FiscalYear { get; } = fiscalYear;
        public InvoiceNumber Number { get; private set; } = number;
        public Address CustomerAddress { get; set; } = customerAddress;
        public DatePeriod PerformancePeriod { get; private set; } = performancePeriod;
        public decimal Tax { get; private set; } = tax;
        public string TaxDetails { get; private set; } = taxDetails;
        public DateOnly? InvoiceDate { get; private set; } = invoiceDate;
        public bool IsReleased { get; private set; } = isReleased;
        public DateTime? ReleasedOn { get; private set; } = releasedOn;
        public OrderId? OrderId { get; private set; } = orderId;
        public OfferId? OfferId { get; private set; } = offerId;
        public List<InvoiceItem> Items { get; } = items;
        public decimal Amount => Items.Sum(x => x.TotalPrice);
        public Currency Currency { get; } = currency;
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

        public override void Delete()
        {
            CheckCanEdit();
        }

        public InvoiceItem AddItem(InvoiceItemId id, string details, Quantity quantity, decimal unitPrice, ProductId? productId = null)
        {
            CheckCanEdit();
            if (string.IsNullOrWhiteSpace(details))
            {
                throw new DomainException("Details dürfen nicht leer sein.", Id);
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
            var itemToMove = Items.Find(x => x.Id == id)!;
            var itemToMoveCurrentIdx = Items.IndexOf(itemToMove);
            if (itemToMoveCurrentIdx == Items.Count - 1)
            {
                throw new DomainException("Bereits am Ende der Liste", Id);
            }
            Items.Remove(itemToMove);
            Items.Insert(itemToMoveCurrentIdx + 1, itemToMove);
            ReorderItems();
        }

        public void MoveItemUp(InvoiceItemId id)
        {
            CheckCanEdit();
            var itemToMove = Items.Find(x => x.Id == id)!;
            var itemToMoveCurrentIdx = Items.IndexOf(itemToMove);
            if (itemToMoveCurrentIdx == 0)
            {
                throw new DomainException("Bereits am Anfang der Liste", Id);
            }
            Items.Remove(itemToMove);
            Items.Insert(itemToMoveCurrentIdx - 1, itemToMove);
            ReorderItems();
        }

        public void Release(InvoiceNumber numberIfEmpty, DateTime? releasedOn)
        {
            CheckCanEdit();
            if (Number.IsTemporary && (numberIfEmpty == null || numberIfEmpty.IsTemporary))
            {
                throw new DomainException("Rechungsnummer ist ungültig", Id);
            }
            if (Number != null && !Number.IsTemporary && Number != numberIfEmpty)
            {
                throw new DomainException("Sobald eine Rechungsnummer vergeben wurde, darf diese nicht mehr verändert werden.", Id);
            }
            if (Items.Count == 0)
            {
                throw new DomainException("Keine Positionen vorhanden", Id);
            }

            Number = Number!.IsTemporary ? numberIfEmpty : Number;
            ReleasedOn = releasedOn.HasValue ? releasedOn : DateTime.Now;
            IsReleased = true;
            if (!PerformancePeriod.To.HasValue) 
            {
                PerformancePeriod = DatePeriod.CreateFrom(PerformancePeriod.From.ToDateTime(TimeOnly.MinValue), DateTime.Now);
            }
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
                throw new DomainException("Rechnung wurde bereits freigegeben.", Id);
            }
        }
    }
}

