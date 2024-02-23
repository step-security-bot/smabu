using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.ProductAggregate;
using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Invoices
{
    public record InvoiceItemDTO : IDTO
    {
        public InvoiceItemDTO()
        {

        }

        public string DisplayName => Position.ToString();
        public InvoiceItemId Id { get; set; }
        public InvoiceId InvoiceId { get; set; }
        public int Position { get; set; }
        public string Details { get; set; }
        public Quantity Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public ProductId? ProductId { get; set; }

        public static InvoiceItemDTO From(InvoiceItem invoiceItem)
        {
            return new()
            {
                Id = invoiceItem.Id,
                InvoiceId = invoiceItem.InvoiceId,
                Position = invoiceItem.Position,
                Details = invoiceItem.Details,
                Quantity = invoiceItem.Quantity,
                UnitPrice = invoiceItem.UnitPrice,
                TotalPrice = invoiceItem.TotalPrice,
                ProductId = invoiceItem.ProductId
            };
        }
    }
}