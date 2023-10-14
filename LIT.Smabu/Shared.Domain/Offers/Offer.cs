using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Contracts;
using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Domain.Shared.Offers;
using LIT.Smabu.Domain.Shared.Products;

namespace LIT.Smabu.Domain.Shared.Invoices
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

        public void Edit(decimal tax, string taxDetails, DateOnly offerDate, DateOnly expiresOn)
        {
            this.Tax = tax;
            this.TaxDetails = taxDetails;
            this.OfferDate = offerDate;
            this.ExpiresOn = expiresOn;
        }

        public OfferItem AddItem(OfferItemId id, string details, Quantity quantity, decimal unitPrice, ProductId? productId = null)
        {
            var position = Items.OrderByDescending(x => x.Position).FirstOrDefault()?.Position + 1 ?? 1;
            var result = new OfferItem(id, Id, position, details, quantity, unitPrice, productId);
            Items.Add(result);
            return result;
        }

        public OfferItem EditItem(OfferItemId id, string details, Quantity quantity, decimal unitPrice)
        {
            var item = this.Items.Find(x => x.Id == id)!;
            item.Edit(details, quantity, unitPrice);
            return item;
        }

        public void RemoveItem(OfferItemId id)
        {
            var item = this.Items.Find(x => x.Id == id)!;
            this.Items.Remove(item);
            this.ReorderItems();
        }

        private void ReorderItems()
        {
            var pos = 1;
            foreach (var item in this.Items.OrderBy(x => x.Position)) 
            { 
                item.EditPosition(pos++);
            }
        }
    }
}

