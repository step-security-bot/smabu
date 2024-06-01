using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.ProductAggregate;
using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.OfferAggregate
{
    public class Offer(OfferId id, CustomerId customerId, OfferNumber number, Address customerAddress,
        DateOnly offerDate, DateOnly expiresOn, Currency currency, decimal tax, string taxDetails,
        List<OfferItem> items) : AggregateRoot<OfferId>
    {
        public override OfferId Id { get; } = id;
        public CustomerId CustomerId { get; } = customerId;
        public OfferNumber Number { get; } = number;
        public Address CustomerAddress { get; private set; } = customerAddress;
        public DateOnly OfferDate { get; private set; } = offerDate;
        public DateOnly ExpiresOn { get; private set; } = expiresOn;
        public Currency Currency { get; } = currency;
        public decimal Tax { get; private set; } = tax;
        public string TaxDetails { get; private set; } = taxDetails;
        public List<OfferItem> Items { get; } = items;
        public decimal Amount => Items.Sum(x => x.TotalPrice);

        public static Offer Create(OfferId id, CustomerId customerId, OfferNumber number, Address customerAddress,
            Currency currency, decimal tax, string taxDetails)
        {
            return new Offer(id, customerId, number, customerAddress,
                DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(14)),
                currency, tax, taxDetails, []);
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
    }
}

