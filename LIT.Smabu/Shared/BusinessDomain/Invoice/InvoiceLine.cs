﻿using System;
using LIT.Smabu.Shared.BusinessDomain.Product;
using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.BusinessDomain.Invoice
{
    public class InvoiceLine : Entity<InvoiceLineId>
    {
        public InvoiceLine(InvoiceLineId id, InvoiceId invoiceId, int position, string details, Quantity quantity, decimal unitPrice, Currency currency, ProductId? productId = null)
        {
            Id = id;
            InvoiceId = invoiceId;
            Position = position;
            Details = details;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Currency = currency;
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

