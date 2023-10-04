using System;
using LIT.Smabu.Shared.Entities.Business.ProductAggregate;

namespace LIT.Smabu.Shared.Entities.Business.InvoiceAggregate
{
    public class InvoiceLine : Entity<InvoiceLineId>
    {
        public InvoiceLine(InvoiceLineId id, InvoiceId invoiceId, int position, string details, Quantity quantity, decimal unitPrice, ProductId? productId = null)
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

        public override InvoiceLineId Id { get; }
        public InvoiceId InvoiceId { get; }
        public int Position { get; private set; }
        public string Details { get; private set; }
        public Quantity Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice { get; private set; }
        public ProductId? ProductId { get; private set; }

        public void UpdateQuantityAndUnitPrice(Quantity quantity, decimal unitPrice)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
            RefreshTotalPrice();
        }

        private void RefreshTotalPrice()
        {
            TotalPrice = Quantity.Value * UnitPrice;
        }
    }
}

