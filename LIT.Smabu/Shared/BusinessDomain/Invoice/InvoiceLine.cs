using System;
using LIT.Smabu.Shared.BusinessDomain.Product;

namespace LIT.Smabu.Shared.BusinessDomain.Invoice
{
    public class InvoiceLine : IInvoiceLine
    {
        public InvoiceLine(InvoiceLineId id, InvoiceId invoiceId, string displayName, int position, string details, Quantity quantity, decimal unitPrice, Currency currency, ProductId? productId = null)
        {
            Id = id;
            InvoiceId = invoiceId;
            DisplayName = displayName;
            Position = position;
            Details = details;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Currency = currency;
            ProductId = productId;
            RefreshTotalPrice();
        }

        public InvoiceLineId Id { get; private set; }
        public string DisplayName { get; private set; }
        public InvoiceId InvoiceId { get; private set; }
        public int Position { get; private set; }
        public string Details { get; private set; }
        public Quantity Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice { get; private set; }
        public Currency Currency { get; private set; }
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

