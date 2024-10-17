using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.ProductAggregate;
using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public class InvoiceItem : Entity<InvoiceItemId>
    {
        public InvoiceItem(InvoiceItemId id, InvoiceId invoiceId, int position, string details, Quantity quantity, decimal unitPrice, ProductId? productId = null)
        {
            Id = id;
            InvoiceId = invoiceId;
            Position = position;
            Details = details;
            Quantity = quantity;
            UnitPrice = unitPrice;
            ProductId = productId;
            RefreshTotalPrice();
        }

        public override InvoiceItemId Id { get; }
        public InvoiceId InvoiceId { get; }
        public int Position { get; private set; }
        public string Details { get; private set; }
        public Quantity Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice { get; private set; }
        public ProductId? ProductId { get; private set; }

        internal void Edit(string details, Quantity quantity, decimal unitPrice)
        {
            Details = details;
            Quantity = quantity;
            UnitPrice = unitPrice;
            RefreshTotalPrice();
        }

        internal void EditPosition(int position)
        {
            Position = position;
        }

        private void RefreshTotalPrice()
        {
            TotalPrice = Quantity.Value * UnitPrice;
        }
    }
}

