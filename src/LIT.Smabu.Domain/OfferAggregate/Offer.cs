using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.Contracts;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.ProductAggregate;

namespace LIT.Smabu.Domain.OfferAggregate
{
    public class Offer : AggregateRoot<OfferId>
    {
        public Offer(OfferId id, CustomerId customerId, OfferNumber number, Address customerAddress,
            DateOnly offerDate, DateOnly expiresOn, Currency currency, decimal tax, string taxDetails, List<OfferItem> items)
        {
            Id = id;
            CustomerId = customerId;
            Number = number;
            CustomerAddress = customerAddress;
            OfferDate = offerDate;
            ExpiresOn = expiresOn;
            Currency = currency;
            Tax = tax;
            TaxDetails = taxDetails;
            Items = items;
        }

        public override OfferId Id { get; }
        public CustomerId CustomerId { get; }
        public OfferNumber Number { get; }
        public Address CustomerAddress { get; private set; }
        public DateOnly OfferDate { get; private set; }
        public DateOnly ExpiresOn { get; private set; }
        public Currency Currency { get; }
        public decimal Tax { get; private set; }
        public string TaxDetails { get; private set; }
        public List<OfferItem> Items { get; }
        public decimal Amount => Items.Sum(x => x.TotalPrice);

        public static Offer Create(OfferId id, CustomerId customerId, OfferNumber number, Address customerAddress,
            Currency currency, decimal tax, string taxDetails)
        {
            return new Offer(id, customerId, number, customerAddress,
                DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(14)), currency, tax, taxDetails, new List<OfferItem>());
        }

        public void Update(decimal tax, string taxDetails, DateOnly offerDate, DateOnly expiresOn)
        {
            Tax = tax;
            TaxDetails = taxDetails;
            OfferDate = offerDate;
            ExpiresOn = expiresOn;
        }

        public OfferItem AddItem(OfferItemId id, string details, Quantity quantity, decimal unitPrice, ProductId? productId = null)
        {
            var position = Items.OrderByDescending(x => x.Position).FirstOrDefault()?.Position + 1 ?? 1;
            var result = new OfferItem(id, Id, position, details, quantity, unitPrice, productId);
            Items.Add(result);
            return result;
        }

        public OfferItem UpdateItem(OfferItemId id, string details, Quantity quantity, decimal unitPrice)
        {
            var item = Items.Find(x => x.Id == id)!;
            item.Edit(details, quantity, unitPrice);
            return item;
        }

        public void RemoveItem(OfferItemId id)
        {
            var item = Items.Find(x => x.Id == id)!;
            Items.Remove(item);
            ReorderItems();
        }

        private void ReorderItems()
        {
            var pos = 1;
            foreach (var item in Items.OrderBy(x => x.Position))
            {
                item.EditPosition(pos++);
            }
        }

        public void Delete()
        {

        }
    }
}

