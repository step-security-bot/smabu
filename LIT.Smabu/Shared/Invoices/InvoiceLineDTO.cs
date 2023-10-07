using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Domain.Shared.Products;
using LIT.Smabu.Shared.Contracts;

namespace LIT.Smabu.Shared.Invoices
{
    public class InvoiceLineDTO : IDTO
    {
        public InvoiceLineId Id { get; set; }
        public InvoiceId InvoiceId { get; set; }
        public int Position { get; set; }
        public string Details { get; set; }
        public Quantity Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public ProductId? ProductId { get; set; }
    }
}