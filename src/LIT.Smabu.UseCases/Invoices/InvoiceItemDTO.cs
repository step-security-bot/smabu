using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.ProductAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Invoices
{
    public record InvoiceItemDTO : IDTO
    {
        public InvoiceItemDTO(InvoiceItemId id, InvoiceId invoiceId, int position, string details, Quantity quantity,
                              decimal unitPrice, decimal totalPrice, ProductId? productId)
        {
            Id = id;
            InvoiceId = invoiceId;
            Position = position;
            Details = details;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = totalPrice;
            ProductId = productId;
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

        public static InvoiceItemDTO Create(InvoiceItem invoiceItem)
        {
            return new(invoiceItem.Id, invoiceItem.InvoiceId, invoiceItem.Position, invoiceItem.Details,
                invoiceItem.Quantity, invoiceItem.UnitPrice, invoiceItem.TotalPrice, invoiceItem.ProductId);
        }
    }
}